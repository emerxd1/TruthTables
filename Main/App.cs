namespace Main
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }
        private delegate bool Operacion(bool p, bool q);
        private void App_Load(object sender, EventArgs e)
        {
            dvgtable.Columns.Add("p", "p");// para agregar columnas al datagridview
            dvgtable.Columns.Add("q", "q");// para agregar columnas al datagridview
            dvgtable.Columns.Add("Resultado", "Resultado");// para agregar columnas al datagridview
            dvgtable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;// para centrar el contenido de las celdas


            rbConjuncion.CheckedChanged += (s, ev) => GenerarTabla();// Agregar el evento CheckedChanged para los RadioButtons
            rbDisyuncion.CheckedChanged += (s, ev) => GenerarTabla();// Agregar el evento CheckedChanged para los RadioButtons
            rbCondicional.CheckedChanged += (s, ev) => GenerarTabla();// Agregar el evento CheckedChanged para los RadioButtons
            rbBicondicional.CheckedChanged += (s, ev) => GenerarTabla();// Agregar el evento CheckedChanged para los RadioButtons

        }
        private void GenerarTabla()
        {
            string nombre;
            string simbolo;
            Operacion funcion;

            if (rbConjuncion.Checked)
            {
                nombre = "Conjuncion";
                simbolo = "AND";
                funcion = (p, q) => p && q;
            }// para la conjuncion logica, el resultado es verdadero si ambos operandos son verdaderos
            else if (rbDisyuncion.Checked)
            {
                nombre = "Disyuncion";
                simbolo = "OR";
                funcion = (p, q) => p || q;
            }// para la disyuncion logica, el resultado es verdadero si al menos uno de los operandos es verdadero
            else if (rbCondicional.Checked)
            {
                nombre = "Condicional";
                simbolo = "->";
                funcion = (p, q) => !p || q;
            }// para la condicional logica, el resultado es falso si el primer operando es verdadero y el segundo es falso
            else
            {
                nombre = "Bicondicional";
                simbolo = "<->";
                funcion = (p, q) => p == q;
            }// para la bicondicional logica, el resultado es verdadero si ambos operandos son iguales

            lblFormula.Text = $"{nombre}:   p {simbolo} q";// para mostrar la formula logica en el label

            dvgtable.Rows.Clear();
            bool[] valores = { true, false };

            foreach (bool p in valores)
            {
                foreach (bool q in valores)
                {
                    bool resultado = funcion(p, q);// Calcular el resultado de la operación lógica
                    int idx = dvgtable.Rows.Add(Texto(p), Texto(q), Texto(resultado));// Agregar una nueva fila al DataGridView con los valores de p, q y el resultado

                    dvgtable.Rows[idx].DefaultCellStyle.ForeColor =
                        resultado ? Color.FromArgb(30, 130, 70) : Color.FromArgb(170, 50, 50);// Cambiar el color de las celdas según el resultado
                    dvgtable.Rows[idx].DefaultCellStyle.Font = new Font("Consolas", 9.5F, FontStyle.Bold);// Cambiar la fuente de las celdas

                }
            }
        }


        private string Texto(bool valor)
        {
            // Ejemplo: devuelve "V" / "F"
            return valor ? "V" : "F";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }
    }
}
