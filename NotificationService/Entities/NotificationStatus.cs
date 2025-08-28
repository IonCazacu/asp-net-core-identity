using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Entities;

public class NotificationStatus
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    protected NotificationStatus()
    {
    }

    public NotificationStatus(string name)
    {
        Name = name;
    }
}