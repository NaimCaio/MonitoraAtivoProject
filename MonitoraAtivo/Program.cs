using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using MonitoraAtivo.Model.Interfaces;
using MonitoraAtivo.Services;
using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using MonitoraAtivo.Model;

namespace MonitoraAtivo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, args);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            
            var applicationService = serviceProvider.GetService<IApplicationService>();

            Console.WriteLine("Começando aplicação");
            applicationService.startApplication();
        }

        public static void ConfigureServices(IServiceCollection services, string[] args)
        {
            

            var config = new ApplicationConfiguration();
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Bind(config);



            var applicationargs = new ApplicationArgs(args[0], args[1], args[2]);

            services.AddSingleton(config);
            services.AddSingleton(applicationargs);
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<IMailService, MailService>();
            //services.AddSingleton<IFinanceService>( new  FinanceService(apiKey,financeApiURI,financeApiTemplate));
        }
    }
}
