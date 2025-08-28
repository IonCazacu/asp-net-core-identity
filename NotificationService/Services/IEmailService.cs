namespace NotificationService.Services;

public interface IEmailService
{
    Task SendEmailAsync();
    // Task SendEmailAsync(string toEmail, string subject, string message);
}