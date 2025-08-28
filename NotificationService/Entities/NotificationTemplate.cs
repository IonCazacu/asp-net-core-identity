using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Entities;

public class NotificationTemplate
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid NotificationTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string EventName { get; set; }
    public string TemplateSubject { get; set; }
    public string TemplateHtmlContent { get; set; }
    public bool IsEnabled { get; set; }
    public NotificationType NotificationType { get; set; }
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    protected NotificationTemplate()
    {
    }

    public NotificationTemplate(NotificationType notificationType, string eventName, string templateSubject,
        string templateHtmlContent, bool isEnabled)
    {
        ArgumentNullException.ThrowIfNull(notificationType);
        NotificationTypeId = notificationType.Id;
        EventName = eventName;
        TemplateSubject = templateSubject;
        TemplateHtmlContent = templateHtmlContent;
        IsEnabled = isEnabled;
        NotificationType = notificationType;
    }
}