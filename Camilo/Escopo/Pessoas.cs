using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AulaEscopo //a classe Pessoas consegue acesar a classe Program, pois pertencem ao mesmo namespace
{
    public class Pessoas
    {
        int idade;
        static string nome;
        string outroNome;

        static void SetName(string name)
        {
            //idade = 10; //error: metodo estatico só tem acesso a metodos ou propriedades estaticas, da classe
            //nome = "Rafael";
            nome = name;

            //outroNome = name; // erro pois não tem acesso a propriedade não estatica da classe

        }
        
        public static void SetPublicName(string name)
        {
            nome = name;

        }

        public string GetName()
        {
            return nome; //nome é estatico mas pode ser acessivel por metodos não estaticos
                         //estaticos não tem acesso aos não estaticos
                         //não estaticos tem acesso aos estaticos (porque os estaticos são compartilhados com o escopo da classe)
        }                //os não estaticos, pertencem apenas ao escopo dos objetos

    }
}
