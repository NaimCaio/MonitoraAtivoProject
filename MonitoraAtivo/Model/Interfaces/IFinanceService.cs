using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Model.Interfaces
{
    public interface IFinanceService
    {
        Task<Ativo> getStockData(string symbol);
    }
}
