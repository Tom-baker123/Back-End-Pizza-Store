namespace Web_Pizza.Services.IServices
{
    public interface IEmailService
    {
        Task SendMailAsync(string toEmail, string subject, string body);
    }
}
