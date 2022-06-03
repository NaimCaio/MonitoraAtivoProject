using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Model
{
    public class Ativo
    {
        public List<Quote> values { get; set; }
    }

    public class Quote
    {
        public DateTime datetime;
        public decimal open;
        public decimal high;
        public decimal low;
        public decimal close;

    }
}
