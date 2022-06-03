using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Models.Configuration
{
    public class FinanceApiConfiguration
    {
        public string FinanceURI { get; set; }
        public string FinanceGetTemplate { get; set; }
        public string FinanceApiKey { get; set; }

    }
}
