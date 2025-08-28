using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Entities;

public class Notification
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid NotificationStatusId { get; set; }
    public Guid NotificationTemplateId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string HtmlContent { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
    public NotificationTemplate NotificationTemplate { get; set; }
    public ICollection<NotificationAttempts> NotificationAttempts { get; set; } = new List<NotificationAttempts>();

    protected Notification()
    {
    }

    public Notification(NotificationStatus notificationStatus, NotificationTemplate notificationTemplate,
        string recipient, string subject,
        string content, string htmlContent)
    {
        ArgumentNullException.ThrowIfNull(notificationStatus);
        ArgumentNullException.ThrowIfNull(notificationTemplate);
        NotificationStatusId = notificationStatus.Id;
        NotificationTemplateId = notificationTemplate.Id;
        Recipient = recipient;
        Subject = subject;
        Content = content;
        HtmlContent = htmlContent;
        NotificationStatus = notificationStatus;
        NotificationTemplate = notificationTemplate;
    }
}