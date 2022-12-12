using Aula14.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula15.Classes
{
    public class Usuario : Base
    {
        public Usuario(string nome, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.CPF = cpf;
        }

        public Usuario() { }

        private static string CaminhoBase()
        {
            return ConfigurationManager.AppSettings["base_usuarios"];
        }

        public override void Gravar()
        {
            var ususarios = Usuario.LerUsuarios();
            Usuario u = new Usuario(this.Nome, this.Telefone, this.CPF);
            ususarios.Add(u);
            if (File.Exists(CaminhoBase()))
            {
                StreamWriter r = new StreamWriter(CaminhoBase());
                r.WriteLine("nome;telefone;cpf;");
                foreach (Usuario us in ususarios)
                {
                    var linha = us.Nome + ";" + us.Telefone + ";" + us.CPF + ";";
                    r.WriteLine(linha);
                }
                r.Close();
            }
        }

        public static List<Usuario> LerUsuarios()
        {
            var usuarios = new List<Usuario>();
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
