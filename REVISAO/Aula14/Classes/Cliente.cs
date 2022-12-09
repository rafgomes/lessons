using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula14.Classes
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        
        public void Gravar()
        {
            if(this.GetType() == typeof(Cliente)) //validar o tipo, ou seja se a instancia for cliente, excecuta o if
            {
                var clientes = Cliente.LerUsuarios();
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
            else
            {                           //////////////////Gambiarra sem usar interface
                var ususarios = Usuario.LerUsuarios();
                Usuario u = new Usuario(this.Nome, this.Telefone, this.CPF);
                ususarios.Add(u);
                if (File.Exists(CaminhoBaseUsuarios()))
                {
                    StreamWriter r = new StreamWriter(CaminhoBaseUsuarios());
                    r.WriteLine("nome;telefone;cpf;");
                    foreach (Usuario us in ususarios)
                    {
                        var linha = us.Nome + ";" + us.Telefone + ";" + us.CPF + ";";
                        r.WriteLine(linha);
                    }
                    r.Close();
                }
            }

        }
        
        private static string CaminhoBaseClientes()
        {
            return ConfigurationManager.AppSettings["base_clientes"];
        }        
        
        private static string CaminhoBaseUsuarios()
        {
            return ConfigurationManager.AppSettings["base_usuarios"];
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

        public static List<Usuario> LerUsuarios()
        {
            var usuarios = new List<Usuario>();
            if (File.Exists(CaminhoBaseUsuarios()))
            {
                using (StreamReader arquivo = File.OpenText(CaminhoBaseUsuarios()))
                {
                    string linha;
                    int i = 0;

                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        i++;
                        if (i == 1) continue;
                        var usuarioArquivo = linha.Split(';');
                        var usuario = new Cliente();
                        usuario.Nome = usuarioArquivo[0];
                        usuario.Telefone = usuarioArquivo[1];
                        usuario.CPF = usuarioArquivo[2];
                        usuarios.Add(usuario);

                    }
                }

            }
            return usuarios;
        }


    }
}
