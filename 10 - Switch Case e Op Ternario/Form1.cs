using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Switch_Case_e_Op_Ternario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int valor = int.Parse(txtValor.Text);

            switch (valor)
            {
                case 10:
                    lblResultado.Text = "É 10!";
                    break;

                case 11:
                    lblResultado.Text = "É 11!";
                    break;
                default:
                    lblResultado.Text = "Não Foi encontrado!";
                   break;
            }

            lblOpTer.Text = valor == 10 ? "É 10 SIM!!!" : "AHH NÃO É 10 🙄";


        }
    }
}
