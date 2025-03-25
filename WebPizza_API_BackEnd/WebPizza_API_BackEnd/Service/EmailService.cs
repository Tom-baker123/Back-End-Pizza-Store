using MailKit.Net.Smtp;
using MimeKit;
using WebPizza_API_BackEnd.Service.IService;
using Microsoft.Extensions.Configuration;

namespace WebPizza_API_BackEnd.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config)); // Kiểm tra null ngay từ đầu
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Lấy cấu hình từ EmailSettings
            var emailSettings = _config.GetSection("EmailSettings");

            // Kiểm tra các giá trị bắt buộc
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPortStr = emailSettings["SmtpPort"];
            var senderName = emailSettings["SenderName"] ?? "Your App"; // Giá trị mặc định nếu null
            var senderEmail = emailSettings["SenderEmail"];
            var username = emailSettings["Username"];
            var password = emailSettings["Password"];

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(senderEmail) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Cấu hình EmailSettings không đầy đủ (SmtpServer, SenderEmail, Username, Password). Vui lòng kiểm tra appsettings.json.");
            }

            if (!int.TryParse(smtpPortStr, out int smtpPort))
            {
                smtpPort = 587; // Giá trị mặc định nếu không parse được
            }

            // Tạo email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            // Gửi email
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Không thể gửi email: {ex.Message}", ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}