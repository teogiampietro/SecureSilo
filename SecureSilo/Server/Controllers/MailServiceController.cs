using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SecureSilo.Server.Data;
using SecureSilo.Server.Models;
using System.Threading.Tasks;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailServiceController : ControllerBase
    {
        private readonly string from = "securesilo@gmail.com";
        private readonly string psw = "securesilo1234";
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MailServiceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public bool SendMessage(string to, string subject, string bodyText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(address: new MailboxAddress(from));

            message.To.Add(address: new MailboxAddress(to));

            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = bodyText
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587);

                client.Authenticate(from, psw);
                client.Send(message);

                client.Disconnect(true);
            }
            return true;
        }

    }
}
