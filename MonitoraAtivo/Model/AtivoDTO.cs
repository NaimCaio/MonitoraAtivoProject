using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Model
{
    public class AtivoDTO
    {
        public List<Quote> values { get; set; }
    }

    public class Quote
    {
        public DateTime datetime;
        public string open;
        public string high;
        public string low;
        public string close;

    }
}
