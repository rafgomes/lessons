using LibTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class FakaPagamento : IPagamentoService
    {
        public void DoPagamento(Banco banco, int valor)
        {
           // throw new NotImplementedException();
        }
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(string email, string conteudo)
        {
///throw new NotImplementedException();
        }
    }

    public class Log : ILoga
    {
        void ILoga.Log(string message)
        {
           // throw new NotImplementedException();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
 
            PagamentosManager teste = new PagamentosManager(new FakaPagamento(), new EmailService(), new Log());
            Banco banco = new Banco();
            banco.NumeroConta = 134;
            banco.Agencia = 12;
            banco.Nome = "itau";
            teste.DoPagamento(banco, 1234, "camilo");

        }
    }
}
