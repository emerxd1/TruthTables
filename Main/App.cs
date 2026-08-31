using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Main
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        // Caracteres permitidos dentro de la operacion logica.
        // (letras para las proposiciones, simbolos de los operadores, parentesis y espacio)
        private const string CaracteresPermitidos = "()¬~!∧^&∨≡<>-| ";

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        // ===========================================================
        //  MOTOR DE EVALUACION DE EXPRESIONES LOGICAS
        //  Soporta: variables de una letra (p, q, r, s, t...),
        //  parentesis anidados y los operadores:
        //     ¬ ~ !          -> NEGACION
        //     ∧ ^ &          -> CONJUNCION (AND)
        //     ∨ v V |        -> DISYUNCION (OR)
        //     -> o →         -> CONDICIONAL
        //     <-> ↔ ≡        -> BICONDICIONAL / CONGRUENCIA
        //  Precedencia (mayor a menor): NOT > AND > OR > -> > <->
        // ===========================================================

        private enum TipoToken { Var, Not, And, Or, Implies, Iff, LParen, RParen, Fin }

        private class Token
        {
            public TipoToken Tipo;
            public string Valor;
            public Token(TipoToken tipo, string valor = "") { Tipo = tipo; Valor = valor; }
        }

        private abstract class Nodo
        {
            public abstract bool Evaluar(Dictionary<string, bool> valores);
            public abstract string Texto();
        }

        private class NodoVar : Nodo
        {
            public string Nombre;
            public NodoVar(string nombre) { Nombre = nombre; }
            public override bool Evaluar(Dictionary<string, bool> valores) => valores[Nombre];
            public override string Texto() => Nombre;
        }

        private class NodoNot : Nodo
        {
            public Nodo Operando;
            public NodoNot(Nodo operando) { Operando = operando; }
            public override bool Evaluar(Dictionary<string, bool> valores) => !Operando.Evaluar(valores);
            public override string Texto()
            {
                string t = Operando.Texto();
                if (Operando is NodoBin) t = "(" + t + ")";
                return "¬" + t;
            }
        }

        private class NodoBin : Nodo
        {
            public Nodo Izq, Der;
            public string Simbolo;
            public Func<bool, bool, bool> Funcion;
            public NodoBin(Nodo izq, Nodo der, string simbolo, Func<bool, bool, bool> funcion)
            { Izq = izq; Der = der; Simbolo = simbolo; Funcion = funcion; }
            public override bool Evaluar(Dictionary<string, bool> valores) => Funcion(Izq.Evaluar(valores), Der.Evaluar(valores));
            public override string Texto() => "(" + Izq.Texto() + " " + Simbolo + " " + Der.Texto() + ")";
        }

        // -----------------------------------------------------------
        //  ANALIZADOR LEXICO (Tokenizer)
        // -----------------------------------------------------------
        private List<Token> Tokenizar(string entrada)
        {
            var tokens = new List<Token>();
            int i = 0;
            entrada = entrada.Trim();

            while (i < entrada.Length)
            {
                char c = entrada[i];

                if (char.IsWhiteSpace(c)) { i++; continue; }
                if (c == '(') { tokens.Add(new Token(TipoToken.LParen)); i++; continue; }
                if (c == ')') { tokens.Add(new Token(TipoToken.RParen)); i++; continue; }

                // Operadores de varios caracteres (revisar primero, son mas largos)
                if (i + 2 < entrada.Length && entrada.Substring(i, 3) == "<->")
                { tokens.Add(new Token(TipoToken.Iff)); i += 3; continue; }

                if (i + 1 < entrada.Length && entrada.Substring(i, 2) == "->")
                { tokens.Add(new Token(TipoToken.Implies)); i += 2; continue; }

                if (c == '↔' || c == '≡') { tokens.Add(new Token(TipoToken.Iff)); i++; continue; }
                if (c == '→') { tokens.Add(new Token(TipoToken.Implies)); i++; continue; }
                if (c == '¬' || c == '~' || c == '!') { tokens.Add(new Token(TipoToken.Not)); i++; continue; }
                if (c == '∧' || c == '^' || c == '&') { tokens.Add(new Token(TipoToken.And)); i++; continue; }
                if (c == '∨' || c == '|') { tokens.Add(new Token(TipoToken.Or)); i++; continue; }

                if (char.IsLetter(c))
                {
                    // La letra "v"/"V" se interpreta como disyuncion (O)
                    if (c == 'v' || c == 'V')
                    { tokens.Add(new Token(TipoToken.Or)); i++; continue; }

                    tokens.Add(new Token(TipoToken.Var, c.ToString().ToLower()));
                    i++;
                    continue;
                }

                throw new Exception($"Caracter no reconocido: '{c}' en la posicion {i + 1}");
            }

            tokens.Add(new Token(TipoToken.Fin));
            return tokens;
        }

        // -----------------------------------------------------------
        //  ANALIZADOR SINTACTICO (parser recursivo descendente)
        // -----------------------------------------------------------
        private List<Token> _tokens;
        private int _pos;
        private Token Actual => _tokens[_pos];

        private Token Consumir(TipoToken tipo)
        {
            if (Actual.Tipo != tipo)
                throw new Exception($"Se esperaba '{tipo}' y se encontro '{Actual.Tipo}'");
            var t = Actual;
            _pos++;
            return t;
        }

        private Nodo ParseExpresion(string entrada)
        {
            _tokens = Tokenizar(entrada);
            _pos = 0;
            var nodo = ParseIff();
            if (Actual.Tipo != TipoToken.Fin)
                throw new Exception("Sobra texto despues de la expresion (revisa operadores u parentesis)");
            return nodo;
        }

        private Nodo ParseIff()
        {
            var izq = ParseImplies();
            while (Actual.Tipo == TipoToken.Iff)
            {
                _pos++;
                var der = ParseImplies();
                izq = new NodoBin(izq, der, "↔", (p, q) => p == q);
            }
            return izq;
        }

        private Nodo ParseImplies()
        {
            var izq = ParseOr();
            if (Actual.Tipo == TipoToken.Implies)
            {
                _pos++;
                var der = ParseImplies(); // asociatividad derecha
                izq = new NodoBin(izq, der, "→", (p, q) => !p || q);
            }
            return izq;
        }

        private Nodo ParseOr()
        {
            var izq = ParseAnd();
            while (Actual.Tipo == TipoToken.Or)
            {
                _pos++;
                var der = ParseAnd();
                izq = new NodoBin(izq, der, "∨", (p, q) => p || q);
            }
            return izq;
        }

        private Nodo ParseAnd()
        {
            var izq = ParseNot();
            while (Actual.Tipo == TipoToken.And)
            {
                _pos++;
                var der = ParseNot();
                izq = new NodoBin(izq, der, "∧", (p, q) => p && q);
            }
            return izq;
        }

        private Nodo ParseNot()
        {
            if (Actual.Tipo == TipoToken.Not)
            {
                _pos++;
                return new NodoNot(ParseNot());
            }
            return ParsePrimario();
        }

        private Nodo ParsePrimario()
        {
            if (Actual.Tipo == TipoToken.Var)
                return new NodoVar(Consumir(TipoToken.Var).Valor);

            if (Actual.Tipo == TipoToken.LParen)
            {
                _pos++;
                var nodo = ParseIff();
                Consumir(TipoToken.RParen);
                return nodo;
            }

            if (Actual.Tipo == TipoToken.Fin)
                throw new Exception("La expresion termina de forma inesperada. Falta una proposicion.");

            throw new Exception("Se esperaba una proposicion (p, q, r...) o '('");
        }

        // -----------------------------------------------------------
        //  Recolectar variables y subexpresiones (columnas de la tabla)
        // -----------------------------------------------------------
        private void RecolectarVariables(Nodo nodo, List<string> variables)
        {
            if (nodo is NodoVar v) { variables.Add(v.Nombre); return; }
            if (nodo is NodoNot n) { RecolectarVariables(n.Operando, variables); return; }
            if (nodo is NodoBin b)
            {
                RecolectarVariables(b.Izq, variables);
                RecolectarVariables(b.Der, variables);
            }
        }

        private void RecolectarColumnas(Nodo nodo, List<Nodo> columnas)
        {
            if (nodo is NodoVar) return; // las variables se muestran aparte, como primeras columnas

            if (nodo is NodoNot n) RecolectarColumnas(n.Operando, columnas);
            if (nodo is NodoBin b)
            {
                RecolectarColumnas(b.Izq, columnas);
                RecolectarColumnas(b.Der, columnas);
            }

            if (!columnas.Any(c => c.Texto() == nodo.Texto()))
                columnas.Add(nodo);
        }

        // -----------------------------------------------------------
        //  VALIDACION EN VIVO (mientras el usuario escribe)
        //  Solo revisa cosas "baratas": vacio y balance de parentesis.
        //  La validacion sintactica completa ocurre al evaluar.
        // -----------------------------------------------------------
        private string ValidarEnVivo(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
                return "Ingresa una operacion logica.";

            int balance = 0;
            foreach (char c in entrada)
            {
                if (c == '(') balance++;
                else if (c == ')')
                {
                    balance--;
                    if (balance < 0)
                        return "Hay un ')' que no tiene su '(' correspondiente.";
                }
            }
            if (balance > 0)
                return $"Falta cerrar {balance} parentesis '('.";

            return null; // sin errores detectables en vivo
        }

        private void ActualizarEstadoValidacion()
        {
            string error = ValidarEnVivo(tbOperadores.Text);

            if (error != null)
            {
                _errorProvider.SetError(tbOperadores, error);
                btnevaluar.Enabled = false;
            }
            else
            {
                _errorProvider.SetError(tbOperadores, "");
                btnevaluar.Enabled = true;
            }
        }

        // -----------------------------------------------------------
        //  Generar la tabla de verdad a partir del texto ingresado
        // -----------------------------------------------------------
        private void GenerarTablaDesdeTexto()
        {
            string entrada = tbOperadores.Text;

            string errorVivo = ValidarEnVivo(entrada);
            if (errorVivo != null)
            {
                MessageBox.Show(errorVivo, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbOperadores.Focus();
                return;
            }

            Nodo raiz;
            try
            {
                raiz = ParseExpresion(entrada);
            }
            catch (Exception ex)
            {
                _errorProvider.SetError(tbOperadores, ex.Message);
                MessageBox.Show("Error en la operacion logica: " + ex.Message,
                    "Error de sintaxis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbOperadores.Focus();
                return;
            }

            var variables = new List<string>();
            RecolectarVariables(raiz, variables);
            variables = variables.Distinct().OrderBy(v => v).ToList();

            if (variables.Count == 0)
            {
                MessageBox.Show("No se encontraron proposiciones (p, q, r...) en la expresion.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (variables.Count > 6)
            {
                MessageBox.Show("Se admiten como maximo 6 proposiciones distintas (2^6 = 64 filas).",
                    "Limite excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var columnasIntermedias = new List<Nodo>();
            RecolectarColumnas(raiz, columnasIntermedias);
            columnasIntermedias.RemoveAll(c => c.Texto() == raiz.Texto());

            dvgtable.Columns.Clear();
            dvgtable.Rows.Clear();

            foreach (var v in variables)
                dvgtable.Columns.Add(v, v.ToUpper());

            foreach (var col in columnasIntermedias)
                dvgtable.Columns.Add(col.Texto(), col.Texto());

            dvgtable.Columns.Add("Resultado", "Resultado");

            lblFormula.Text = "Operacion ingresada: " + raiz.Texto();
            _errorProvider.SetError(tbOperadores, "");

            int n = variables.Count;
            int filas = 1 << n;

            for (int f = 0; f < filas; f++)
            {
                var valores = new Dictionary<string, bool>();
                for (int b = 0; b < n; b++)
                {
                    bool val = ((f >> (n - 1 - b)) & 1) == 0; // 0 -> V, 1 -> F
                    valores[variables[b]] = val;
                }

                var celdas = new List<string>();
                foreach (var v in variables)
                    celdas.Add(Texto(valores[v]));

                foreach (var col in columnasIntermedias)
                    celdas.Add(Texto(col.Evaluar(valores)));

                bool resultado = raiz.Evaluar(valores);
                celdas.Add(Texto(resultado));

                int idx = dvgtable.Rows.Add(celdas.ToArray());

                dvgtable.Rows[idx].DefaultCellStyle.ForeColor =
                    resultado ? Color.FromArgb(30, 130, 70) : Color.FromArgb(170, 50, 50);
                dvgtable.Rows[idx].DefaultCellStyle.Font = new Font("Consolas", 9.5F, FontStyle.Bold);
            }

            dvgtable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private string Texto(bool valor) => valor ? "V" : "F";

        // -----------------------------------------------------------
        //  EVENTOS DEL FORMULARIO
        // -----------------------------------------------------------
        private void App_Load(object sender, EventArgs e)
        {
            dvgtable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btnevaluar.Enabled = false; // no hay nada que evaluar todavia

            // --- Validacion mientras el usuario escribe ---
            tbOperadores.KeyPress += (s, ev) =>
            {
                // Siempre permitir teclas de control (Backspace, etc.)
                if (char.IsControl(ev.KeyChar)) return;

                bool esLetra = char.IsLetter(ev.KeyChar);
                bool esSimboloValido = CaracteresPermitidos.IndexOf(ev.KeyChar) >= 0;

                if (!esLetra && !esSimboloValido)
                    ev.Handled = true; // bloquea el caracter (numeros, signos de puntuacion, etc.)
            };

            tbOperadores.TextChanged += (s, ev) => ActualizarEstadoValidacion();

            tbOperadores.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true; // evita el "beep"
                    if (btnevaluar.Enabled) GenerarTablaDesdeTexto();
                }
            };

            // --- Boton Evaluar ---
            btnevaluar.Click += (s, ev) => GenerarTablaDesdeTexto();

            // --- Boton Limpiar ---
            btnClear.Click += (s, ev) =>
            {
                tbOperadores.Clear();
                dvgtable.Columns.Clear();
                dvgtable.Rows.Clear();
                lblFormula.Text = "";
                _errorProvider.SetError(tbOperadores, "");
                tbOperadores.Focus();
            };

            // --- Botones de proposiciones (insertan la letra) ---
            AsignarInsercion(btnp, "p");
            AsignarInsercion(btnq, "q");
            AsignarInsercion(btnr, "r");
            AsignarInsercion(btns, "s");

            // --- Botones de operadores (insertan el simbolo) ---
            AsignarInsercion(btnNegacion, "¬");
            AsignarInsercion(btnOR, "V");
            AsignarInsercion(btnAND, "∧");
            AsignarInsercion(btnEntonces, "→");
            AsignarInsercion(btnBicondicional, "↔");
            AsignarInsercion(btnCongruencia, "≡"); // equivalencia logica (misma tabla que la bicondicional)
        }

        private void AsignarInsercion(Control control, string texto)
        {
            if (control == null) return;
            control.Click += (s, e) =>
            {
                int pos = tbOperadores.SelectionStart;
                tbOperadores.Text = tbOperadores.Text.Insert(pos, texto);
                tbOperadores.SelectionStart = pos + texto.Length;
                tbOperadores.Focus();
            };
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnevaluar_Click(object sender, EventArgs e)
        {

        }
    }
}