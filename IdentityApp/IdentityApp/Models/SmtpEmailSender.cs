
using System.Net;
using System.Net.Mail;

namespace IdentityApp.Models
{
    public class SmtpEmailSender : IEmailSender
    {
        private string? _host;
        private int _port;
        private bool _enableSSL;
        private string? _username;
        private string? _password;
        public SmtpEmailSender(string? host, int port, bool enableSSL, string? username, string? password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _username = username;
            _password = password;
        
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient(_host, _port))
            {
                client.EnableSsl = _enableSSL;
                client.Credentials = new NetworkCredential(_username, _password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_username),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
      
    }
}
