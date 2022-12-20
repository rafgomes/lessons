using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibTesting
{

    public class Banco
    {
        public int NumeroConta { get; set; }
        public string Nome { get; set; }
        public int Agencia { get; set; }

    }

    public interface IPagamentoService{
        void DoPagamento(Banco banco, int valor);
    }

    public interface IEmailService
    {
        void SendEmail(string email, string conteudo);
    }

    public interface ILoga
    {
        void Log(string message);
    }



    public class PagamentosManager
    {
        private readonly IPagamentoService pagamentoService;
        private readonly IEmailService emailService;
        private readonly ILoga logger;

        public PagamentosManager(IPagamentoService pagamentoService, IEmailService emailService, ILoga logger)
        {
            if (pagamentoService == null || emailService == null || logger == null)
                throw new ArgumentNullException("Não pode inserir parametros nulos");

            this.pagamentoService = pagamentoService;
            this.emailService = emailService;
            this.logger = logger;
        }

        public void DoPagamento(Banco banco, int valor, string email)
        {
            try
            {
                pagamentoService.DoPagamento(banco, valor);
                emailService.SendEmail(email, $"O pagamento de {valor} foi emitido com sucesso");

            }catch(Exception e)
            {
                emailService.SendEmail(email, $"Houve um problema com o pagamento de {valor}");
                logger.Log(e.Message);
            }
        }
    }
}
