using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LearnClasses
{
    internal class NomeCompleto
    {
        public string nome;
        public string sobrenome;

        public void NomeInteiro()
        {
            MessageBox.Show(Resposta());
        }

        private string Resposta()
        {
            string completo = nome + " " + sobrenome;
            return completo;
        }
    }
}
