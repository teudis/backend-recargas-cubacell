using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Identity.Services
{
    public class MessageServices : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public MessageServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
       

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Data Server Mail
            var host = _configuration["Email:Host"];
            var port = int.Parse(_configuration["Email:Port"]);
            var email_address = _configuration["Email:Email"];
            var password = _configuration["Email:Password"];

            // Plug in your email service here to send an email.
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("CubanConexions Contact SmartSolucionesCuba", email_address));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text =  htmlMessage };

            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(host, port, false);


                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    //client.Authenticate(email_address, password);

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            });
        }
       
    }
}
