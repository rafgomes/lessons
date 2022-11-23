using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnClasses
{
    internal class Pessoas
    {
        public string nome;
        public int idade;
        public int altura;
        private string nacionalidade;

        public void Falar()
        {
            MessageBox.Show("Olá eu sou a pessoa que voce criou.");
        }

        public void Apresentar()
        {
            MessageBox.Show($"Ola, Sou {nome}, e tenho {idade} anos.");
        }


    }
}
