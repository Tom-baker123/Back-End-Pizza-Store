using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Web_Pizza.Configurations;
using Web_Pizza.Services.IServices;

namespace Web_Pizza.Services
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendMailAsync(string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Gửi mail thành công.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Lỗi SMTP: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");

                }

            }
        }
    }
}
