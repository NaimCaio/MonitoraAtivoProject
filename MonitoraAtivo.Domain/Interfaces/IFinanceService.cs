using MonitoraAtivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Interfaces
{
    public interface IFinanceService
    {
        Task<Ativo> GetStockData(string symbol);
    }
}
