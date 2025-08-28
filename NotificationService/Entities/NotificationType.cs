using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Entities;

public class NotificationType
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public ICollection<NotificationTemplate> NotificationTemplates { get; set; } = new List<NotificationTemplate>();

    protected NotificationType()
    {
    }

    public NotificationType(string name)
    {
        Name = name;
    }

    protected bool Equals(NotificationType other)
    {
        return Id == other.Id;
    }
}