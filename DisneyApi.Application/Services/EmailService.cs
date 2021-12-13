using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace DisneyApi.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailTo);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string emailTo)
        {
            var apiKey = _configuration["EmailKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("disneychallenguejlf@gmail.com", "Registro exitoso! Disney Challengue");
            var subject = "Registro Exitoso";
            var to = new EmailAddress(emailTo, "Cliente");
            var plainTextContent = "Test";
            var htmlContent = "<p>¡Hola!</p>" +
                "<p> Bienvenido! Te has registrado correctamente en el challengue de disney.</p>" +
                "<p>Este es un mensaje automatico. No necesita responderlo</p>" +
                "<p>Alkemy Challengue-Fernandez Jose Luis</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
