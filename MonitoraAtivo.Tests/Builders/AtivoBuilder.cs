using MonitoraAtivo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Tests.Builders
{
    public class AtivoBuilder
    {
        private readonly Ativo ativo;
        public AtivoBuilder()
        {
            ativo = new Ativo();
            ativo.values = new List<Quote>()
            {
                new Quote()
            };
        }

        public AtivoBuilder WithDateTime(DateTime date)
        {
            ativo.values[0].datetime = date;
            return this;
        }
        public AtivoBuilder WithValue(decimal value)
        {
            ativo.values[0].close = value;
            return this;
        }

        public  Ativo Build()
        {
            return ativo;
        }


    }
}
