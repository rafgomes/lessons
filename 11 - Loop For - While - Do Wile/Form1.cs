using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11___Loop_For
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //FOR            
            //for (int valor=0; valor <= 50; valor++)
            //{
            //    lista.Items.Add(valor);
            //}

            //WHILE
            //int vezes = 0;
            //do
            //{
            //    lista.Items.Add(vezes);
            //    vezes++;
            //} while (vezes <= 100);

            //FOR EACH
            //string frase = "Testando FROEACH";
            //foreach (char letra in frase)
            //{
            //    lista.Items.Add(letra);
            //}

            //LIST
            //List<string> ls = new List<string>()
            //{
            //    "Marcel", "Furlan", "Girino", "Cassim", "*-*-*-*-*-*"
            //};
            //foreach (string palavra in ls)
            //{
            //    lista.Items.Add(palavra);
            //}

            //ARRAY
            //int[] valores;
            //valores= new int[5]; //5 elementos dentro do array
            //valores[]



        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            lista.Items.Clear();
        }
    }
}
