using Relatorios.Entity;
using System.Collections.Generic;

namespace Relatorios.Dados
{
    public interface IRepositorio
    {
        List<Cliente> GetClients(string empresa);
    }
}