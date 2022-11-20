using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operadores
{
    public partial class Form1 : Form
    {
        float Nota1, Nota2, Nota3, Nota4, media;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Nota1 = float.Parse(txtNota1.Text);
            Nota2 = float.Parse(txtNota2.Text);
            Nota3 = float.Parse(txtNota3.Text);
            Nota4 = float.Parse(txtNota4.Text);
            media = (Nota1 + Nota2 + Nota3 + Nota4) / 4;

            txtMedia.Text = Convert.ToString(media);

            //Situação
            if (media >= 7) 
            {
                lblStatus.Text = "APROVADO";            
            } else
            {
                lblStatus.Text = "REPROVADO";
            }

        }
    }
}
