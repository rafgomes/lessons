using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncoesRecursivas.Classes
{
    public class Cliente
    {
        #region Construtores
        //public Cliente() //Construtor sempre tem o mesmo nome da classe
        //{
        //    this.Nome = "Camilo"; //significa que toda vez que eu criar a instancia da classe, quero que a propriedade Nome venha preenchida com "camilo"

        //}

        //public Cliente(string _nome) //obriga a carregar o parametro Nome
        //{
        //    this.Nome = _nome;
        //}

        //public Cliente(string _telefone) //não pode ter dois construtores com a mesma assinatura (string), mesmo que seja utilizando parametros diferentes, para isso teria que ser public "Cliente(string _telefone, string Nome)"
        //{
        //    this.Telefone = _telefone;
        //}
        #endregion

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        #region Gravar 1
        /*
        public static void Gravar()
        {
            var clientes = Cliente.LerClientes();
            clientes.Add(this); //adiciona toda a instancia daquele objeto que acabou de criar

            if (File.Exists(CaminhoBaseClientes()))
            {
                string conteudo = "nome;telefone;cpf;\n"; //toda vez que criar o objeto, vai escrever esse cabeçalho e pular uma linha
                foreach (Cliente c in clientes)
                {
                    conteudo += c.Nome + ";" + c.Telefone + ";" + c.CPF + ";\n";
                }

                File.WriteAllText(CaminhoBaseClientes(), conteudo);
            }
        }
        */
        #endregion

        #region Gravar 2
        
        public static void Gravar()  //outra maneira de gravar
        {
            var clientes = Cliente.LerClientes();
            clientes.Add(this);
            if (File.Exists(CaminhoBaseClientes()))
            {
                StreamWriter r = new StreamWriter(CaminhoBaseClientes());
                r.WriteLine("nome;telefone;cpf;");
                foreach (Cliente c in clientes)
                {
                    var linha = c.Nome + ";" + c.Telefone + ";" + c.CPF + ";";
                    r.WriteLine(linha);
                }
                r.Close();
            }
        }
        
        #endregion

        private static string CaminhoBaseClientes()
        {
            return ConfigurationManager.AppSettings["base_clientes"];
        }

        public static List<Cliente> LerClientes()
        {
            var clientes = new List<Cliente>();
            if (File.Exists(CaminhoBaseClientes()))
            {
                using (StreamReader arquivo = File.OpenText(CaminhoBaseClientes()))
                {
                    string linha;
                    int i = 0;

                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        i++;
                        if (i == 1) continue; //ignorando a primeira linha do arquivo
                        var clienteArquivo = linha.Split(';'); //identificando que as propriedades no arquivo estão separadas por ponto e virgula //para identificar byte usa-se aspas simples

                        //var cliente = new Cliente { Nome = clienteArquivo[0], Telefone = clienteArquivo[1], CPF = clienteArquivo[2] }; exemplo de como instanciar ja passando os valores, economizando as 3 linhas como abaixo:
                        
                        //var cliente = new Cliente { Nome = clienteArquivo[0],
                        //    Telefone = clienteArquivo[1],                         //tambem pode fazer quebrando as linhas "entre as virgulas" para ficar mais legivel
                        //    CPF = clienteArquivo[2]
                        //};

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
