using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RiluCrocidilu.Services
{
    public class EmailSender : IEmailSender
    {
      
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }
      
    }
}
