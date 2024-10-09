namespace Snake
{
    partial class Form1
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
            panel1 = new Panel();
            btnMultijugador = new Button();
            ColorSecundario = new ComboBox();
            ColorPrincipal = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            Iniciar = new Button();
            nvlDiff = new ComboBox();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnMultijugador);
            panel1.Controls.Add(ColorSecundario);
            panel1.Controls.Add(ColorPrincipal);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Iniciar);
            panel1.Controls.Add(nvlDiff);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(561, 216);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnMultijugador
            // 
            btnMultijugador.Location = new Point(439, 147);
            btnMultijugador.Name = "btnMultijugador";
            btnMultijugador.Size = new Size(107, 29);
            btnMultijugador.TabIndex = 7;
            btnMultijugador.Text = "Multijugador";
            btnMultijugador.UseVisualStyleBackColor = true;
            btnMultijugador.Click += btnMultijugador_Click;
            // 
            // ColorSecundario
            // 
            ColorSecundario.FormattingEnabled = true;
            ColorSecundario.Items.AddRange(new object[] { "Rojo", "Verde", "Azul", "Amarillo", "Naranja", "Morado" });
            ColorSecundario.Location = new Point(395, 72);
            ColorSecundario.Name = "ColorSecundario";
            ColorSecundario.Size = new Size(151, 28);
            ColorSecundario.TabIndex = 6;
            ColorSecundario.SelectedIndexChanged += ColorSecundario_SelectedIndexChanged;
            // 
            // ColorPrincipal
            // 
            ColorPrincipal.FormattingEnabled = true;
            ColorPrincipal.Items.AddRange(new object[] { "Rojo", "Verde", "Azul", "Amarillo", "Naranja", "Morado" });
            ColorPrincipal.Location = new Point(208, 72);
            ColorPrincipal.Name = "ColorPrincipal";
            ColorPrincipal.Size = new Size(151, 28);
            ColorPrincipal.TabIndex = 5;
            ColorPrincipal.SelectedIndexChanged += ColorPrincipal_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(395, 33);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 4;
            label3.Text = "Color secundario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(208, 33);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 3;
            label2.Text = "Color principal";
            // 
            // Iniciar
            // 
            Iniciar.Location = new Point(22, 147);
            Iniciar.Name = "Iniciar";
            Iniciar.Size = new Size(94, 29);
            Iniciar.TabIndex = 2;
            Iniciar.Text = "Iniciar";
            Iniciar.UseVisualStyleBackColor = true;
            Iniciar.Click += button1_Click;
            // 
            // nvlDiff
            // 
            nvlDiff.FormattingEnabled = true;
            nvlDiff.Items.AddRange(new object[] { "Facil", "Medio", "Dificil" });
            nvlDiff.Location = new Point(22, 72);
            nvlDiff.Name = "nvlDiff";
            nvlDiff.Size = new Size(151, 28);
            nvlDiff.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 33);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 0;
            label1.Text = "Dificultad";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 443);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ComboBox nvlDiff;
        private Label label1;
        private Button Iniciar;
        private Label label3;
        private Label label2;
        private ComboBox ColorSecundario;
        private ComboBox ColorPrincipal;
        private Button btnMultijugador;
    }
}
