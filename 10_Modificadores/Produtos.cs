using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modificadores
{
    /*
        Modificadores de acesso permite determinar o nivel de acessibilidade dos membros e tipos de um metodo,
        definindo como eles podem, ou não, serem acessados por outros metodos ou a partir de outros assemblies.
    
    
        public -> Atributos e Metodos Visiveis em Qualquer Classe
        private -> Atributos e Metodos Visiveis apenas na classe onde são criados
        protected -> Atributos e Metodos Visiveis nas classes onde são criados ou herdados
     */

    internal class Produtos
    {
        public string nome;
        private double preco;



    }
}
