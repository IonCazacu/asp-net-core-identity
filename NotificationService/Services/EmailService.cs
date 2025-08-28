using System.Text;
using System.Text.Json;

namespace NotificationService.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _turbosmtpSettings;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        _turbosmtpSettings = configuration.GetSection("TurboSMTPSettings");
    }

    public async Task SendEmailAsync()
    {
        var consumerKey = _turbosmtpSettings["ConsumerKey"];
        var consumerSecret = _turbosmtpSettings["ConsumerSecret"];

        const string url = "https://api.turbo-smtp.com/api/v2/mail/send";

        var mailData = new
        {
            from = "cazacuion791@gmail.com",
            to = "cazacuion99@mail.com", // ,contact@global-travel.com
            subject = "New live training session",
            // cc = "cc_user@example.com",
            // bcc = "bcc_user@example.com",
            content =
                "Dear partner, we are delighted to invite you to an exclusive training session on UX Design. This session is designed to provide essential insights and practical strategies to enhance your skills.",
            html_content =
                "Dear partner, We are delighted to invite you to an exclusive training session on <strong>UX Design</strong>. This session is designed to provide essential insights and practical strategies to enhance your skills."
        }; // Mail Data setup.

        using (HttpClient httpClient = new HttpClient())
        {
            // JSON data seriaization
            var json = JsonSerializer.Serialize(mailData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set authentication headers
            content.Headers.Add("consumerKey", consumerKey);
            content.Headers.Add("consumerSecret", consumerSecret);

            // Trigger POST request
            using (var response = await httpClient.PostAsync(url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + result);
                }
                else
                {
                    Console.WriteLine("Request error: " + response.StatusCode);
                }
            }
        }
    }
}