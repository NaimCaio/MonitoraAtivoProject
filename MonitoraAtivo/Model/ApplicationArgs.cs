using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Model
{
    public class ApplicationArgs
    {
        public readonly string Symbol;
        public readonly decimal BuyPrice;
        public readonly decimal SellPrice; 

        public ApplicationArgs(string symbol, decimal sellPrice, decimal buyPrice)
        {
            Symbol = symbol;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }
    }
}
