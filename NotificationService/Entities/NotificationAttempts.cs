using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Entities;

public class NotificationAttempts
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid NotificationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    // api specific response
    public string Status { get; private set; }
    public string ProviderResponse { get; private set; }
    public Notification Notification { get; set; }

    protected NotificationAttempts()
    {
    }

    public NotificationAttempts(Notification notification, string status, string providerResponse)
    {
        ArgumentNullException.ThrowIfNull(notification);
        NotificationId = notification.Id;
        Notification = notification;
        Status = status;
        ProviderResponse = providerResponse;
    }
}