using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MonitoraAtivo.Model;
using MonitoraAtivo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Services
{
    class MailService: IMailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ApplicationConfiguration _config;
        public MailService(ApplicationConfiguration config)
        {
            _config = config;
        }

        

        public async Task<string> SendEmailAsync(string title, string content)
        {
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
                smtp.Connect(_config.MailConfiguration.Host, _config.MailConfiguration.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.MailConfiguration.Sender, _config.MailConfiguration.Password);
                var response =smtp.Send(email);
                smtp.Disconnect(true);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
