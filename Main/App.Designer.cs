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
            rbDisyuncion = new RadioButton();
            rbConjuncion = new RadioButton();
            rbCondicional = new RadioButton();
            rbBicondicional = new RadioButton();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            dvgtable = new DataGridView();
            lblFormula = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dvgtable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // rbDisyuncion
            // 
            rbDisyuncion.AutoSize = true;
            rbDisyuncion.Font = new Font("Century Gothic", 9.75F);
            rbDisyuncion.Location = new Point(53, 81);
            rbDisyuncion.Name = "rbDisyuncion";
            rbDisyuncion.Size = new Size(146, 21);
            rbDisyuncion.TabIndex = 0;
            rbDisyuncion.TabStop = true;
            rbDisyuncion.Text = "Disyuncion (p or q)";
            rbDisyuncion.UseVisualStyleBackColor = true;
            // 
            // rbConjuncion
            // 
            rbConjuncion.AutoSize = true;
            rbConjuncion.Font = new Font("Century Gothic", 9.75F);
            rbConjuncion.Location = new Point(53, 106);
            rbConjuncion.Name = "rbConjuncion";
            rbConjuncion.Size = new Size(166, 21);
            rbConjuncion.TabIndex = 1;
            rbConjuncion.TabStop = true;
            rbConjuncion.Text = "Conjuncion (p and q)";
            rbConjuncion.UseVisualStyleBackColor = true;
            // 
            // rbCondicional
            // 
            rbCondicional.AutoSize = true;
            rbCondicional.Font = new Font("Century Gothic", 9.75F);
            rbCondicional.Location = new Point(53, 131);
            rbCondicional.Name = "rbCondicional";
            rbCondicional.Size = new Size(131, 21);
            rbCondicional.TabIndex = 2;
            rbCondicional.TabStop = true;
            rbCondicional.Text = "Condicional (->)";
            rbCondicional.UseVisualStyleBackColor = true;
            // 
            // rbBicondicional
            // 
            rbBicondicional.AutoSize = true;
            rbBicondicional.Font = new Font("Century Gothic", 9.75F);
            rbBicondicional.Location = new Point(53, 156);
            rbBicondicional.Name = "rbBicondicional";
            rbBicondicional.Size = new Size(146, 21);
            rbBicondicional.TabIndex = 3;
            rbBicondicional.TabStop = true;
            rbBicondicional.Text = "Bicondicional (<->)";
            rbBicondicional.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(64, 59);
            label1.Name = "label1";
            label1.Size = new Size(247, 19);
            label1.TabIndex = 4;
            label1.Text = "Seleccione el operador logico:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 50);
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
            dvgtable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgtable.Location = new Point(12, 237);
            dvgtable.Name = "dvgtable";
            dvgtable.ReadOnly = true;
            dvgtable.RowHeadersVisible = false;
            dvgtable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgtable.Size = new Size(360, 124);
            dvgtable.TabIndex = 6;
            // 
            // lblFormula
            // 
            lblFormula.AutoSize = true;
            lblFormula.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFormula.ForeColor = Color.Blue;
            lblFormula.Location = new Point(34, 203);
            lblFormula.Name = "lblFormula";
            lblFormula.Size = new Size(143, 17);
            lblFormula.TabIndex = 7;
            lblFormula.Text = "Conjuncion: p and q";
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(378, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 24);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(348, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(24, 24);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 421);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(lblFormula);
            Controls.Add(dvgtable);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(rbBicondicional);
            Controls.Add(rbCondicional);
            Controls.Add(rbConjuncion);
            Controls.Add(rbDisyuncion);
            FormBorderStyle = FormBorderStyle.None;
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

        private RadioButton rbDisyuncion;
        private RadioButton rbConjuncion;
        private RadioButton rbCondicional;
        private RadioButton rbBicondicional;
        private Label label1;
        private PictureBox pictureBox1;
        private DataGridView dvgtable;
        private Label lblFormula;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}
