using MonitoraAtivo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Models
{
    public class MonitoradorAtivo: ISubject
    {
        private List<IObserver> _observers;
        public Quote actualQuote { 
            get {
                return _actualQuote;
            } set {
                _lastQuote = _actualQuote;
                _actualQuote = value;
                Notify();
            } }

        private Quote _actualQuote { get; set; }
        private Quote _lastQuote { get; set; }
        public MonitoradorAtivo()
        {
            _observers = new List<IObserver>();
        }

        

        public void Atach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.VerifyIfPriceHits(this,_lastQuote);
            });
        }
    }
}
