using MonitoraAtivo.Model;
using MonitoraAtivo.Model.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace MonitoraAtivo.Services
{
    public class FinanceService: IFinanceService
    {
        private readonly ApplicationConfiguration _config;

        public FinanceService(ApplicationConfiguration config)
        {
            _config = config;
        }

        public async Task<AtivoDTO> getStockData(string symbol)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.ApiConfiguration.FinanceURI);
            httpClient.DefaultRequestHeaders.Add("X-API-KEY",
                "<your API key>");
            httpClient.DefaultRequestHeaders.Add("accept",
                "application/json");

            var url = _config.ApiConfiguration.FinanceGetTemplate.Replace("#REQUESTESYMBOL#", symbol).Replace("#USERAPIKEY#", _config.ApiConfiguration.FinanceApiKey);

            //var response = await httpClient.GetAsync(
            //    url);

            //var responseBody = await response.Content.ReadAsStringAsync();
            //var quotes = JsonConvert.DeserializeObject<AtivoDTO>(responseBody);
            var quotes = new AtivoDTO()
            {
                values = new List<Quote>()
                {
                    new Quote()
                    {
                        close="100",
                        datetime= DateTime.Now
                       
                    }
                }
            };
            

            return quotes;
        }

        //public async Task<int> getStockData(string simbulo)
        //{
            
        //    try
        //    {
        //        var data = await Yahoo.GetHistoricalAsync(simbulo, DateTime.Now, DateTime.Now.AddMonths(-6));
        //        var security = await Yahoo.Symbols(simbulo).Fields(Field.LongName).QueryAsync();
        //        var ticker = security[simbulo];
        //        var companyName = ticker[Field.LongName];
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            Console.WriteLine(data.ElementAt(i).Close);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        var a = 2;
        //        throw;
        //    }
        //    return 1;

        //}
    }
}
