using System.Diagnostics;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendMailApp.Helper;

namespace SendMailApp.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings ;
        public EmailService(IOptions<EmailSettings> options) {
            this.emailSettings = options.Value;
        }
        
        public async Task<string> SendEmailAsync(Mailrequest mailrequest)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
            email.Subject = mailrequest.Subject;
            var builder = new BodyBuilder();

            if (mailrequest.Adjunto != null)
            {
            
                Stream file = new MemoryStream(mailrequest.Adjunto);
                builder.Attachments.Add("terminos_y_condiciones.pdf", file, ContentType.Parse("application/octet-stream"));
            }

            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase).Replace(@"file:\","") +@"\Templates\"+mailrequest.NombrePlantilla+".html";
            if (File.Exists(path))
            {
                var lines = await File.ReadAllLinesAsync(path);
                string content = string.Join("", lines);

                builder.HtmlBody = content;
            }
            else
            {
            builder.HtmlBody = "<h1> No existe la plantilla</h1>";
                
            }
            email.Body = builder.ToMessageBody();



            using var smtp = new SmtpClient();
            smtp.Connect(emailSettings.Host, emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Email, emailSettings.Password);
            smtp.Timeout =emailSettings.Timeout;
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            stopwatch.Stop();
            return stopwatch.Elapsed.ToString();
        }
    }
}
