using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class AdminLog
{
    public int Id { get; set; }

    public int AdminUserId { get; set; }
    public AppUser? AdminUser { get; set; }

    [Required, StringLength(80)]
    public string Action { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string EntityName { get; set; } = string.Empty;

    public int EntityId { get; set; }

    [StringLength(220)]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
