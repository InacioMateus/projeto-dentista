namespace projeto_dentista
{
    partial class MenuDentibao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDentistas = new System.Windows.Forms.Button();
            this.btnPacientes = new System.Windows.Forms.Button();
            this.btnConsultas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDentistas
            // 
            this.btnDentistas.Location = new System.Drawing.Point(41, 41);
            this.btnDentistas.Name = "btnDentistas";
            this.btnDentistas.Size = new System.Drawing.Size(139, 57);
            this.btnDentistas.TabIndex = 0;
            this.btnDentistas.Text = "Dentistas";
            this.btnDentistas.UseVisualStyleBackColor = true;
            this.btnDentistas.Click += new System.EventHandler(this.btnDentistas_Click);
            // 
            // btnPacientes
            // 
            this.btnPacientes.Location = new System.Drawing.Point(41, 161);
            this.btnPacientes.Name = "btnPacientes";
            this.btnPacientes.Size = new System.Drawing.Size(139, 57);
            this.btnPacientes.TabIndex = 1;
            this.btnPacientes.Text = "Pacientes";
            this.btnPacientes.UseVisualStyleBackColor = true;
            this.btnPacientes.Click += new System.EventHandler(this.btnPacientes_Click);
            // 
            // btnConsultas
            // 
            this.btnConsultas.Location = new System.Drawing.Point(41, 281);
            this.btnConsultas.Name = "btnConsultas";
            this.btnConsultas.Size = new System.Drawing.Size(139, 57);
            this.btnConsultas.TabIndex = 2;
            this.btnConsultas.Text = "Consultas";
            this.btnConsultas.UseVisualStyleBackColor = true;
            this.btnConsultas.Click += new System.EventHandler(this.btnConsultas_Click);
            // 
            // MenuDentibao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 401);
            this.Controls.Add(this.btnConsultas);
            this.Controls.Add(this.btnPacientes);
            this.Controls.Add(this.btnDentistas);
            this.Name = "MenuDentibao";
            this.Text = "MenuDentibao";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDentistas;
        private System.Windows.Forms.Button btnPacientes;
        private System.Windows.Forms.Button btnConsultas;
    }
}