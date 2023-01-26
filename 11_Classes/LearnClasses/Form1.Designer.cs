namespace LearnClasses
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNome = new System.Windows.Forms.Label();
            this.lblAltura = new System.Windows.Forms.Label();
            this.lblIdade = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtBoxNome = new System.Windows.Forms.TextBox();
            this.txtBoxSobre = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 18);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(16, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "...";
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.Location = new System.Drawing.Point(12, 48);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(16, 13);
            this.lblAltura.TabIndex = 1;
            this.lblAltura.Text = "...";
            // 
            // lblIdade
            // 
            this.lblIdade.AutoSize = true;
            this.lblIdade.Location = new System.Drawing.Point(12, 79);
            this.lblIdade.Name = "lblIdade";
            this.lblIdade.Size = new System.Drawing.Size(16, 13);
            this.lblIdade.TabIndex = 2;
            this.lblIdade.Text = "...";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(69, 255);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtBoxNome
            // 
            this.txtBoxNome.Location = new System.Drawing.Point(15, 107);
            this.txtBoxNome.Name = "txtBoxNome";
            this.txtBoxNome.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNome.TabIndex = 4;
            // 
            // txtBoxSobre
            // 
            this.txtBoxSobre.Location = new System.Drawing.Point(15, 143);
            this.txtBoxSobre.Name = "txtBoxSobre";
            this.txtBoxSobre.Size = new System.Drawing.Size(100, 20);
            this.txtBoxSobre.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 290);
            this.Controls.Add(this.txtBoxSobre);
            this.Controls.Add(this.txtBoxNome);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblIdade);
            this.Controls.Add(this.lblAltura);
            this.Controls.Add(this.lblNome);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.Label lblIdade;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtBoxNome;
        private System.Windows.Forms.TextBox txtBoxSobre;
    }
}

