using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace PetShop.Application.Services.OtherServices
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void SendMail(string forMail, string code)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassWord = _configuration["EmailSettings:SenderPassword"];


            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Authomatize", senderEmail));
            message.To.Add(new MailboxAddress("", forMail));
            message.Subject = "Código Teste de verificação";
            message.Body = new TextPart("html") { Text = $"O código de teste é: <b>{code}<b>" };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(senderEmail, senderPassWord);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
