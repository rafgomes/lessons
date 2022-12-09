using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula15.Classes
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        
        public virtual void Gravar() //virtual é um metodo que pode ser sobrescrito //para nunca deixar sobrescrever tem que ser seled
        {
                var clientes = Cliente.LerClientes();
                clientes.Add(this);
                if (File.Exists(CaminhoBase()))
                {
                    StreamWriter r = new StreamWriter(CaminhoBase());
                    r.WriteLine("nome;telefone;cpf;");
                    foreach (Cliente c in clientes)
                    {
                        var linha = c.Nome + ";" + c.Telefone + ";" + c.CPF + ";";
                        r.WriteLine(linha);
                    }
                    r.Close();
                }
        }
        
        private static string CaminhoBase()
        {
            return ConfigurationManager.AppSettings["base_clientes"];
        }        

        public static List<Cliente> LerClientes()
        {
            var clientes = new List<Cliente>();
            if (File.Exists(CaminhoBase()))
            {
                using (StreamReader arquivo = File.OpenText(CaminhoBase()))
                {
                    string linha;
                    int i = 0;

                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        i++;
                        if (i == 1) continue;
                        var clienteArquivo = linha.Split(';');
                        var cliente = new Cliente();
                        cliente.Nome = clienteArquivo[0];
                        cliente.Telefone = clienteArquivo[1];
                        cliente.CPF = clienteArquivo[2];
                        clientes.Add(cliente);
                        
                    }
                }
                
            }
            return clientes;
        }

        


    }
}
