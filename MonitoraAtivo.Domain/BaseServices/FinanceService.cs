using MonitoraAtivo.Domain.Interfaces;
using MonitoraAtivo.Domain.Models;
using MonitoraAtivo.Domain.Models.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.BaseServices
{
    public class FinanceService: IFinanceService
    {
        private readonly ApplicationConfiguration _config;

        public FinanceService(ApplicationConfiguration config)
        {
            _config = config;
        }

        public async Task<Ativo> GetStockData(string symbol)
        {
            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.ApiConfiguration.FinanceURI);
            httpClient.DefaultRequestHeaders.Add("X-API-KEY",
                "<your API key>");
            httpClient.DefaultRequestHeaders.Add("accept",
                "application/json");


            var url = _config.ApiConfiguration.FinanceGetTemplate.Replace("#REQUESTESYMBOL#", symbol).Replace("#USERAPIKEY#", _config.ApiConfiguration.FinanceApiKey);
            
            var quotes = new Ativo();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
                quotes = JsonConvert.DeserializeObject<Ativo>(responseText);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            return quotes;
        }

    }
}
