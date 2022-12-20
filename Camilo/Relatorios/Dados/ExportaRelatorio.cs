using Relatorios.Clientes;
using Relatorios.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class ExportaRelatorio
    {
        private readonly IRelatorio relatorio;

        public ExportaRelatorio(IRelatorio relatorio)
        {
            this.relatorio = relatorio;
        }

        public void ExportaTXT()
        {
            string resultTXT = relatorio.GetRelatorio();

            Console.WriteLine(resultTXT);

            // File.WriteAllText($@"C:\Projets\Lessons\Camilo\Relatorios\DB{relatorio.Nome}_Relatorio.txt", resultTXT);

        }
    }

    public interface IProducto
    {       
        decimal getPrecoComfrete();
    }

    public abstract class ProdutoPai {
        public void FazAlgoParaTodos() {
            Console.WriteLine("Faz algo para todo mundo");
        }

        public abstract  decimal getPrecoComfrete();
    }

    public class Filha : ProdutoPai
    {
        public override decimal getPrecoComfrete()
        {
            base.FazAlgoParaTodos();

            return 190;

        }
    }



    public class Girino : IProducto
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal getPrecoComfrete()
        {
            throw new NotImplementedException();
        }
    }



    public class Digital : IProducto
    {


        public decimal Frete = 0;

        public decimal PrecoAtual = 0;

        public string Name { get; set; }

        public decimal getPrecoComfrete()
        {
            return Frete + PrecoAtual;
        }
    }

    public class LivroDital : Digital
    {
        public LivroDital(string nomedoProduto)
        {
            base.Name = nomedoProduto;
        }
        public decimal getPrecoComfrete()
        {
            //logica de negocio pegar o preco no banco

            decimal valorDoBanco = 1234;
            base.PrecoAtual = valorDoBanco;


            return base.getPrecoComfrete();
        }
    }

    public class Musica : Digital
    {
        public Musica(string nomedoProduto)
        {
            base.Name = nomedoProduto;
        }
        public decimal getPrecoComfrete()
        {
            //logica de negocio pegar o preco no banco

            decimal valorDoBanco = 1234;
            base.PrecoAtual = valorDoBanco;


            return base.getPrecoComfrete();
        }
    }

    public class DispachaProduto
    {
        private readonly IProducto producto;

        public DispachaProduto(IProducto producto)
        {
            this.producto = producto;
        }

        public void DisparcharPara(string endereco)
        {
            Console.WriteLine($"Enviar produto {producto.Name} para {endereco}");
            Console.WriteLine($"Receber valor de {producto.getPrecoComfrete()}");
        }

    }





    public class Program
    {

        public static void Main(string[] args)
        {
            var produtco = new LivroDital("Acao Humana - Mises");

            var dispacha = new DispachaProduto(produtco);
            dispacha.DisparcharPara("Rua Xaraies,34, Ituvaerava-sp ");
            var musica = new Musica("Stairway to Heaven -Led Zeppeling");
            var dispacha2 = new DispachaProduto(musica);
            dispacha2.DisparcharPara("Rua Xaraies,34, Ituvaerava-sp ");




        }


    }









}
