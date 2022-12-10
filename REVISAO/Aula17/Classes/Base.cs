using Aula17.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula17
{
    public class Base : IPessoa
    {
        public Base(string nome, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.CPF = cpf;
        }

        public Base() { }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public void SetNome(string nome) { this.Nome = nome; }
        public void SetTelefone(string telefone) { this.Telefone= telefone; }
        public void SetCPF(string cpf) { this.CPF = cpf; }

        public void Gravar() //virtual é um metodo que pode ser sobrescrito //para nunca deixar sobrescrever tem que ser seled
        {
            var dados = this.Ler();
            dados.Add(this);

            StreamWriter r = new StreamWriter(DiretorioComArquivo());
            r.WriteLine("nome;telefone;cpf;");
            foreach (Base b in dados)
            {
                var linha = b.Nome + ";" + b.Telefone + ";" + b.CPF + ";";
                r.WriteLine(linha);
            }
            r.Close();

        }

        public List<IPessoa> Ler()
        {
            var dados = new List<IPessoa>();
            if (File.Exists(DiretorioComArquivo()))
            {
                using (StreamReader arquivo = File.OpenText(DiretorioComArquivo()))
                {
                    string linha;
                    int i = 0;

                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        i++;
                        if (i == 1) continue;
                        var baseArquivo = linha.Split(';');

                        var b = (IPessoa)Activator.CreateInstance(this.GetType());
                        b.SetNome(baseArquivo[0]);
                        b.SetTelefone(baseArquivo[1]);
                        b.SetCPF(baseArquivo[2]);
                        dados.Add(b);
                    }
                }
            }
            return dados;
        }

        private string DiretorioComArquivo()
        {
            return ConfigurationManager.AppSettings["CaminhoArquivos"] + this.GetType().Name + ".txt";
        }
    }
}
