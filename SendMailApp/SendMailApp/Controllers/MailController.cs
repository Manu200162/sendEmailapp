using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendMailApp.Helper;
using SendMailApp.Service;

namespace SendMailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService emailService;
        public MailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] Mailrequest request) 
        {
            try
            {

            //Mailrequest mailrequest = new Mailrequest();
            //mailrequest.ToEmail = "manuel.valenzuela.del@gmail.com";
            //mailrequest.Subject = "Sendind an email test";
            //mailrequest.Body = "<h1> Eres un gran programador solo no te rindas</h1>";
            string res = await emailService.SendEmailAsync(request);
            return Ok(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
