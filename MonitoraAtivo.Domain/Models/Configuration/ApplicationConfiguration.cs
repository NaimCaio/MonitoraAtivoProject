using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Models.Configuration
{
    public class ApplicationConfiguration
    {
        public FinanceApiConfiguration ApiConfiguration { get; set; }
        public MailConfiguration MailConfiguration { get; set; }
    }
}
