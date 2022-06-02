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
        public readonly string BuyPrice;
        public readonly string SellPrice; 

        public ApplicationArgs(string symbol, string buyPrice, string sellPrice)
        {
            Symbol = symbol;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }
    }
}
