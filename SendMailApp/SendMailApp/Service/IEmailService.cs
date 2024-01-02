using SendMailApp.Helper;

namespace SendMailApp.Service
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(Mailrequest mailrequest);
    }
}
