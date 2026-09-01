namespace Main
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            pictureBox1 = new PictureBox();
            dvgtable = new DataGridView();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            lblFormula = new Label();
            label1 = new Label();
            tbOperadores = new TextBox();
            btnBicondicional = new Button();
            btnCongruencia = new Button();
            btnEntonces = new Button();
            btnAND = new Button();
            btnNegacion = new Button();
            btnOR = new Button();
            btnq = new Button();
            btnr = new Button();
            btnp = new Button();
            btnevaluar = new Button();
            btnClear = new Button();
            btns = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dvgtable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 57);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // dvgtable
            // 
            dvgtable.AllowUserToAddRows = false;
            dvgtable.AllowUserToDeleteRows = false;
            dvgtable.AllowUserToResizeRows = false;
            dvgtable.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dvgtable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgtable.BackgroundColor = SystemColors.ButtonFace;
            dvgtable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgtable.Location = new Point(12, 398);
            dvgtable.Name = "dvgtable";
            dvgtable.ReadOnly = true;
            dvgtable.RowHeadersVisible = false;
            dvgtable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgtable.Size = new Size(872, 141);
            dvgtable.TabIndex = 6;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(880, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 27);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(850, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(24, 27);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // lblFormula
            // 
            lblFormula.AutoSize = true;
            lblFormula.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFormula.ForeColor = Color.Blue;
            lblFormula.Location = new Point(34, 360);
            lblFormula.Name = "lblFormula";
            lblFormula.Size = new Size(205, 17);
            lblFormula.TabIndex = 7;
            lblFormula.Text = "Operacion ingresada: p and q";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(322, 10);
            label1.Name = "label1";
            label1.Size = new Size(227, 19);
            label1.TabIndex = 4;
            label1.Text = "Ingrese la operacion logica:";
            // 
            // tbOperadores
            // 
            tbOperadores.BorderStyle = BorderStyle.FixedSingle;
            tbOperadores.Location = new Point(297, 35);
            tbOperadores.Name = "tbOperadores";
            tbOperadores.Size = new Size(297, 22);
            tbOperadores.TabIndex = 10;
            // 
            // btnBicondicional
            // 
            btnBicondicional.FlatAppearance.BorderSize = 0;
            btnBicondicional.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnBicondicional.FlatStyle = FlatStyle.Flat;
            btnBicondicional.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnBicondicional.Image = (Image)resources.GetObject("btnBicondicional.Image");
            btnBicondicional.Location = new Point(552, 248);
            btnBicondicional.Name = "btnBicondicional";
            btnBicondicional.Size = new Size(109, 60);
            btnBicondicional.TabIndex = 11;
            btnBicondicional.Text = "Bicondicional";
            btnBicondicional.TextImageRelation = TextImageRelation.ImageAboveText;
            btnBicondicional.UseVisualStyleBackColor = true;
            // 
            // btnCongruencia
            // 
            btnCongruencia.FlatAppearance.BorderSize = 0;
            btnCongruencia.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnCongruencia.FlatStyle = FlatStyle.Flat;
            btnCongruencia.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnCongruencia.Image = (Image)resources.GetObject("btnCongruencia.Image");
            btnCongruencia.Location = new Point(678, 248);
            btnCongruencia.Name = "btnCongruencia";
            btnCongruencia.Size = new Size(103, 60);
            btnCongruencia.TabIndex = 12;
            btnCongruencia.Text = "Congruencia";
            btnCongruencia.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCongruencia.UseVisualStyleBackColor = true;
            // 
            // btnEntonces
            // 
            btnEntonces.FlatAppearance.BorderSize = 0;
            btnEntonces.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnEntonces.FlatStyle = FlatStyle.Flat;
            btnEntonces.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnEntonces.Image = (Image)resources.GetObject("btnEntonces.Image");
            btnEntonces.Location = new Point(451, 248);
            btnEntonces.Name = "btnEntonces";
            btnEntonces.Size = new Size(95, 60);
            btnEntonces.TabIndex = 13;
            btnEntonces.Text = "Entonces";
            btnEntonces.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEntonces.UseVisualStyleBackColor = true;
            // 
            // btnAND
            // 
            btnAND.FlatAppearance.BorderSize = 0;
            btnAND.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnAND.FlatStyle = FlatStyle.Flat;
            btnAND.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnAND.Image = (Image)resources.GetObject("btnAND.Image");
            btnAND.Location = new Point(350, 247);
            btnAND.Name = "btnAND";
            btnAND.Size = new Size(95, 60);
            btnAND.TabIndex = 16;
            btnAND.Text = "Conjuncion";
            btnAND.TextImageRelation = TextImageRelation.ImageAboveText;
            btnAND.UseVisualStyleBackColor = true;
            // 
            // btnNegacion
            // 
            btnNegacion.FlatAppearance.BorderSize = 0;
            btnNegacion.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnNegacion.FlatStyle = FlatStyle.Flat;
            btnNegacion.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnNegacion.Image = (Image)resources.GetObject("btnNegacion.Image");
            btnNegacion.Location = new Point(154, 247);
            btnNegacion.Name = "btnNegacion";
            btnNegacion.Size = new Size(95, 60);
            btnNegacion.TabIndex = 15;
            btnNegacion.Text = "Negacion";
            btnNegacion.TextImageRelation = TextImageRelation.ImageAboveText;
            btnNegacion.UseVisualStyleBackColor = true;
            // 
            // btnOR
            // 
            btnOR.FlatAppearance.BorderSize = 0;
            btnOR.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnOR.FlatStyle = FlatStyle.Flat;
            btnOR.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnOR.Image = (Image)resources.GetObject("btnOR.Image");
            btnOR.Location = new Point(244, 247);
            btnOR.Name = "btnOR";
            btnOR.Size = new Size(95, 60);
            btnOR.TabIndex = 14;
            btnOR.Text = "Disyuncion";
            btnOR.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOR.UseVisualStyleBackColor = true;
            // 
            // btnq
            // 
            btnq.FlatAppearance.BorderSize = 0;
            btnq.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnq.FlatStyle = FlatStyle.Flat;
            btnq.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnq.Image = (Image)resources.GetObject("btnq.Image");
            btnq.Location = new Point(359, 157);
            btnq.Name = "btnq";
            btnq.Size = new Size(95, 60);
            btnq.TabIndex = 17;
            btnq.Text = "Proposicion";
            btnq.TextImageRelation = TextImageRelation.ImageAboveText;
            btnq.UseVisualStyleBackColor = true;
            // 
            // btnr
            // 
            btnr.FlatAppearance.BorderSize = 0;
            btnr.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnr.FlatStyle = FlatStyle.Flat;
            btnr.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnr.Image = (Image)resources.GetObject("btnr.Image");
            btnr.Location = new Point(486, 157);
            btnr.Name = "btnr";
            btnr.Size = new Size(95, 60);
            btnr.TabIndex = 19;
            btnr.Text = "Proposicion";
            btnr.TextImageRelation = TextImageRelation.ImageAboveText;
            btnr.UseVisualStyleBackColor = true;
            // 
            // btnp
            // 
            btnp.FlatAppearance.BorderSize = 0;
            btnp.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnp.FlatStyle = FlatStyle.Flat;
            btnp.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnp.Image = (Image)resources.GetObject("btnp.Image");
            btnp.Location = new Point(243, 157);
            btnp.Name = "btnp";
            btnp.Size = new Size(95, 60);
            btnp.TabIndex = 20;
            btnp.Text = "Proposicion";
            btnp.TextImageRelation = TextImageRelation.ImageAboveText;
            btnp.UseVisualStyleBackColor = true;
            // 
            // btnevaluar
            // 
            btnevaluar.BackColor = Color.RoyalBlue;
            btnevaluar.FlatAppearance.BorderSize = 0;
            btnevaluar.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnevaluar.FlatStyle = FlatStyle.Flat;
            btnevaluar.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnevaluar.Image = (Image)resources.GetObject("btnevaluar.Image");
            btnevaluar.Location = new Point(346, 83);
            btnevaluar.Name = "btnevaluar";
            btnevaluar.Size = new Size(99, 39);
            btnevaluar.TabIndex = 21;
            btnevaluar.Text = "Evaluar";
            btnevaluar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnevaluar.UseVisualStyleBackColor = false;
            btnevaluar.Click += btnevaluar_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.IndianRed;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnClear.Image = (Image)resources.GetObject("btnClear.Image");
            btnClear.Location = new Point(480, 83);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(101, 39);
            btnClear.TabIndex = 22;
            btnClear.Text = "Limpiar";
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = false;
            // 
            // btns
            // 
            btns.FlatAppearance.BorderSize = 0;
            btns.FlatAppearance.MouseOverBackColor = Color.Silver;
            btns.FlatStyle = FlatStyle.Flat;
            btns.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btns.Image = (Image)resources.GetObject("btns.Image");
            btns.Location = new Point(603, 157);
            btns.Name = "btns";
            btns.Size = new Size(95, 60);
            btns.TabIndex = 23;
            btns.Text = "Proposicion";
            btns.TextImageRelation = TextImageRelation.ImageAboveText;
            btns.UseVisualStyleBackColor = true;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(916, 609);
            Controls.Add(btns);
            Controls.Add(btnClear);
            Controls.Add(btnevaluar);
            Controls.Add(btnp);
            Controls.Add(btnr);
            Controls.Add(btnq);
            Controls.Add(btnAND);
            Controls.Add(btnNegacion);
            Controls.Add(btnOR);
            Controls.Add(btnEntonces);
            Controls.Add(btnCongruencia);
            Controls.Add(btnBicondicional);
            Controls.Add(tbOperadores);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(lblFormula);
            Controls.Add(dvgtable);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "App";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "App";
            Load += App_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dvgtable).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private DataGridView dvgtable;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label lblFormula;
        private Label label1;
        private TextBox tbOperadores;
        private Button btnBicondicional;
        private Button btnCongruencia;
        private Button btnEntonces;
        private Button btnAND;
        private Button btnNegacion;
        private Button btnOR;
        private Button btnq;
        private Button btnr;
        private Button btnp;
        private Button btnevaluar;
        private Button btnClear;
        private Button btns;
    }
}
