using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MonitoraAtivo.Domain.Interfaces;
using MonitoraAtivo.Domain.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.BaseServices
{
    public class MailService: IMailService
    {
        private readonly ApplicationConfiguration _config;
        public MailService(ApplicationConfiguration config)
        {
            _config = config;
        }

        

        public async Task<string> SendEmailAsync(string title, string content)
        {
            Console.WriteLine(content);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_config.MailConfiguration.Sender);
            foreach (var to in _config.MailConfiguration.To)
            {
                email.To.Add(MailboxAddress.Parse(to));
            }
            
            email.Subject = title;
            var builder = new BodyBuilder();
            
            builder.HtmlBody = content;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            try
            {
                Console.WriteLine("Enviando e-mail");
                smtp.Connect(_config.MailConfiguration.Host, _config.MailConfiguration.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.MailConfiguration.Sender, _config.MailConfiguration.Password);
                var response = await smtp.SendAsync(email);
                smtp.Disconnect(true);
                Console.WriteLine("E-mail enviado");
                return response;
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
