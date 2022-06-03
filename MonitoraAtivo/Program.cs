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
using System.Threading;
using System.Globalization;

namespace MonitoraAtivo
{
    class Program
    {
        private static EventWaitHandle WaitApplication = new EventWaitHandle(false, EventResetMode.ManualReset);
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, args);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            
            var applicationService = serviceProvider.GetService<IApplicationService>();

            Console.WriteLine("Começando aplicação");
            applicationService.startApplication();
            WaitApplication.WaitOne();
        }

        public static void ConfigureServices(IServiceCollection services, string[] args)
        {
            

            var config = new ApplicationConfiguration();
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Bind(config);



            var applicationargs = new ApplicationArgs(args[0], decimal.Parse(args[1], CultureInfo.InvariantCulture), decimal.Parse(args[2], CultureInfo.InvariantCulture));

            services.AddSingleton(config);
            services.AddSingleton(applicationargs);
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<IMailService, MailService>();
            //services.AddSingleton<IFinanceService>( new  FinanceService(apiKey,financeApiURI,financeApiTemplate));
        }
    }
}
