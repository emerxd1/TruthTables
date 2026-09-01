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

        // Caracteres que el usuario puede escribir en el textbox
        private const string CaracteresPermitidos = "()¬!∧^&∨≡<>-| ";

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        // Tipos de token que puede generar el analizador lexico
        private enum TipoToken { Var, Not, And, Or, Implies, Iff, LParen, RParen, Fin }

        // Un token guarda su tipo y, si aplica, el texto asociado (ej: nombre de variable)
        private class Token
        {
            public TipoToken Tipo;
            public string Valor;
            public Token(TipoToken tipo, string valor = "") { Tipo = tipo; Valor = valor; }
        }

        // Nodo base del arbol de sintaxis (AST)
        private abstract class Nodo
        {
            public abstract bool Evaluar(Dictionary<string, bool> valores);
            public abstract string Texto();
        }

        // Nodo hoja: representa una proposicion (p, q, r,s)
        private class NodoVar : Nodo // clase hija de nodo
        {
            public string Nombre;
            public NodoVar(string nombre) { Nombre = nombre; } //pasa la letra y almacena en la variable Nombre
            public override bool Evaluar(Dictionary<string, bool> valores) => valores[Nombre]; // devuelve el valor de la proposicion (V o F) segun el diccionario
            public override string Texto() => Nombre;
        }

        // Nodo unario: negacion de otro nodo
        private class NodoNot : Nodo// clase hija de nodo
        {
            public Nodo Operando;//almacena el nodo que se va a negar
            public NodoNot(Nodo operando) { Operando = operando; }//constructor que recibe el nodo a negar
            public override bool Evaluar(Dictionary<string, bool> valores) => !Operando.Evaluar(valores);//devuelve el valor negado del nodo que se esta evaluando
            public override string Texto()//devuelve el texto del nodo negado, agregando parentesis si es un nodo binario
            {
                // Si el operando es binario se envuelve en parentesis para claridad
                string t = Operando.Texto();
                if (Operando is NodoBin) t = "(" + t + ")";
                return "¬" + t;
            }
        }

        // Nodo binario: aplica una funcion logica (AND, OR, etc.) entre dos nodos
        private class NodoBin : Nodo
        {
            public Nodo Izq, Der;//almacena los nodos izquierdo y derecho
            public string Simbolo;//almacena el simbolo del operador logico
            public Func<bool, bool, bool> Funcion;//almacena la funcion logica que se va a aplicar entre los nodos izquierdo y derecho
            public NodoBin(Nodo izq, Nodo der, string simbolo, Func<bool, bool, bool> funcion)//constructor que recibe los nodos izquierdo y derecho, el simbolo del operador logico y la funcion logica a aplicar
            { Izq = izq; Der = der; Simbolo = simbolo; Funcion = funcion; }//almacena los valores recibidos en las variables de instancia
            public override bool Evaluar(Dictionary<string, bool> valores) => Funcion(Izq.Evaluar(valores), Der.Evaluar(valores));//devuelve el resultado de aplicar la funcion logica entre los valores de los nodos izquierdo y derecho
            public override string Texto() => "(" + Izq.Texto() + " " + Simbolo + " " + Der.Texto() + ")";//devuelve el texto del nodo binario, agregando parentesis para claridad
        }

        // Convierte el texto ingresado en una lista de tokens
        private List<Token> Tokenizar(string entrada)
        {
            var tokens = new List<Token>();
            int i = 0;
            entrada = entrada.Trim();

            while (i < entrada.Length)
            {
                char c = entrada[i];

                if (char.IsWhiteSpace(c)) { i++; continue; } // ignora espacios

                if (c == '(') { tokens.Add(new Token(TipoToken.LParen)); i++; continue; }
                if (c == ')') { tokens.Add(new Token(TipoToken.RParen)); i++; continue; }

                // Se revisan primero los operadores de mas de un caracter
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
                    // "v"/"V" se toma como disyuncion (O) en vez de variable
                    if (c == 'v' || c == 'V')
                    { tokens.Add(new Token(TipoToken.Or)); i++; continue; }

                    tokens.Add(new Token(TipoToken.Var, c.ToString().ToLower()));
                    i++;
                    continue;
                }

                throw new Exception($"Caracter no reconocido: '{c}' en la posicion {i + 1}");
            }

            tokens.Add(new Token(TipoToken.Fin)); // marca de fin de cadena
            return tokens;
        }

        // Parser recursivo descendente: variables de instancia para la posicion actual
        private List<Token> _tokens;
        private int _pos;
        private Token Actual => _tokens[_pos];

        // Verifica que el token actual sea del tipo esperado y avanza
        private Token Consumir(TipoToken tipo)
        {
            if (Actual.Tipo != tipo)
                throw new Exception($"Se esperaba '{tipo}' y se encontro '{Actual.Tipo}'");
            var t = Actual;
            _pos++;
            return t;
        }

        // Punto de entrada del parser: tokeniza y arma el arbol completo
        private Nodo ParseExpresion(string entrada)
        {
            _tokens = Tokenizar(entrada);
            _pos = 0;
            var nodo = ParseIff();
            if (Actual.Tipo != TipoToken.Fin)
                throw new Exception("Sobra texto despues de la expresion (revisa operadores u parentesis)");
            return nodo;
        }

        // Nivel de menor precedencia: bicondicional (<->)
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

        // Condicional (->), asociativo a la derecha
        private Nodo ParseImplies()
        {
            var izq = ParseOr();
            if (Actual.Tipo == TipoToken.Implies)
            {
                _pos++;
                var der = ParseImplies();
                izq = new NodoBin(izq, der, "→", (p, q) => !p || q);
            }
            return izq;
        }

        // Disyuncion (OR)
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

        // Conjuncion (AND)
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

        // Negacion, mayor precedencia
        private Nodo ParseNot()
        {
            if (Actual.Tipo == TipoToken.Not)
            {
                _pos++;
                return new NodoNot(ParseNot());
            }
            return ParsePrimario();
        }

        // Unidad basica: una variable o una expresion entre parentesis
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

        // Recorre el arbol y junta los nombres de variables usadas
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

        // Recorre el arbol y junta las subexpresiones (para mostrarlas como columnas)
        private void RecolectarColumnas(Nodo nodo, List<Nodo> columnas)
        {
            if (nodo is NodoVar) return; // las variables van aparte, como primeras columnas

            if (nodo is NodoNot n) RecolectarColumnas(n.Operando, columnas);
            if (nodo is NodoBin b)
            {
                RecolectarColumnas(b.Izq, columnas);
                RecolectarColumnas(b.Der, columnas);
            }

            // evita agregar la misma subexpresion dos veces
            if (!columnas.Any(c => c.Texto() == nodo.Texto()))
                columnas.Add(nodo);
        }

        // Valida cosas basicas mientras el usuario escribe (vacio y parentesis)
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

            return null;
        }

        // Actualiza el mensaje de error y habilita/deshabilita el boton Evaluar
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

        // Genera toda la tabla de verdad y muestra el tipo de proposicion (tautologia, contradiccion o contingencia)
        private void GenerarTablaDesdeTexto()
        {
            string entrada = tbOperadores.Text;

            // validacion rapida antes de intentar parsear
            string errorVivo = ValidarEnVivo(entrada);
            if (errorVivo != null)
            {
                MessageBox.Show(errorVivo, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbOperadores.Focus();
                return;
            }

            // parseo de la expresion, captura errores de sintaxis
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

            // obtiene las variables usadas, ordenadas alfabeticamente
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

            // subexpresiones intermedias que se mostraran como columnas extra
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
            int filas = 1 << n; // 2^n combinaciones posibles

            // guarda todos los resultados finales para saber si es tautologia/contradiccion/contingencia
            var resultadosFinales = new List<bool>();

            for (int f = 0; f < filas; f++)
            {
                // arma la combinacion de valores V/F para esta fila
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
                resultadosFinales.Add(resultado);
                celdas.Add(Texto(resultado));

                int idx = dvgtable.Rows.Add(celdas.ToArray());

                // pinta la fila de verde si es V y rojo si es F, para leerla mas rapido
                dvgtable.Rows[idx].DefaultCellStyle.ForeColor =
                    resultado ? Color.FromArgb(30, 130, 70) : Color.FromArgb(170, 50, 50);
                dvgtable.Rows[idx].DefaultCellStyle.Font = new Font("Consolas", 9.5F, FontStyle.Bold);
            }

            dvgtable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // muestra si la expresion es tautologia, contradiccion o contingencia
            MostrarTipoDeProposicion(resultadosFinales);
        }

        // Analiza la columna de resultados y clasifica la expresion
        private void MostrarTipoDeProposicion(List<bool> resultados)
        {
            bool todasVerdaderas = resultados.All(r => r);
            bool todasFalsas = resultados.All(r => !r);

            string tipo;
            Color color;

            if (todasVerdaderas)
            {
                tipo = "Tautologia (siempre verdadero)";
                color = Color.FromArgb(30, 130, 70);
            }
            else if (todasFalsas)
            {
                tipo = "Contradiccion (siempre falso)";
                color = Color.FromArgb(170, 50, 50);
            }
            else
            {
                tipo = "Contingencia (depende de los valores)";
                color = Color.FromArgb(60, 90, 160);
            }

            lblResultado.Text = "Resultado Final: " + tipo;
            lblResultado.ForeColor = color;
        }

        // Convierte un booleano a "V" o "F" para mostrarlo en la tabla
        private string Texto(bool valor) => valor ? "V" : "F";

        private void App_Load(object sender, EventArgs e)
        {
            dvgtable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btnevaluar.Enabled = false; // no hay nada que evaluar todavia

            // bloquea caracteres que no son letras ni simbolos permitidos
            tbOperadores.KeyPress += (s, ev) =>
            {
                if (char.IsControl(ev.KeyChar)) return; // deja pasar Backspace, etc.

                bool esLetra = char.IsLetter(ev.KeyChar);
                bool esSimboloValido = CaracteresPermitidos.IndexOf(ev.KeyChar) >= 0;

                if (!esLetra && !esSimboloValido)
                    ev.Handled = true;
            };

            // revalida cada vez que cambia el texto
            tbOperadores.TextChanged += (s, ev) => ActualizarEstadoValidacion();

            // permite evaluar con la tecla Enter
            tbOperadores.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true; // evita el "beep"
                    if (btnevaluar.Enabled) GenerarTablaDesdeTexto();
                }
            };

            // boton Evaluar: genera la tabla de verdad
            btnevaluar.Click += (s, ev) => GenerarTablaDesdeTexto();

            // boton Limpiar: reinicia el textbox, la tabla y las etiquetas de resultado
            btnClear.Click += (s, ev) =>
            {
                tbOperadores.Clear();
                dvgtable.Columns.Clear();
                lblFormula.Text = "Operacion ingresada:";
                dvgtable.Rows.Clear();
                lblResultado.Text = "Resultado Final:";
                lblResultado.ForeColor = Color.Black;
                _errorProvider.SetError(tbOperadores, "");
                tbOperadores.Focus();
            };

            // botones de proposiciones: insertan la letra correspondiente
            AsignarInsercion(btnp, "p");
            AsignarInsercion(btnq, "q");
            AsignarInsercion(btnr, "r");
            AsignarInsercion(btns, "s");

            // botones de operadores: insertan el simbolo correspondiente
            AsignarInsercion(btnNegacion, "¬");
            AsignarInsercion(btnOR, "V");
            AsignarInsercion(btnAND, "∧");
            AsignarInsercion(btnEntonces, "→");
            AsignarInsercion(btnBicondicional, "↔");
            AsignarInsercion(btnCongruencia, "≡"); // equivalencia logica (misma tabla que la bicondicional)
        }

        // Inserta "texto" en el textbox en la posicion actual del cursor
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

        // Cierra la aplicacion (boton "X" personalizado)
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Minimiza la ventana (boton "minimizar" personalizado)
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnevaluar_Click(object sender, EventArgs e)
        {

        }
    }
}
