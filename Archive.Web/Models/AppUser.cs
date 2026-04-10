using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class AppUser
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }

    [Required, StringLength(30)]
    public string UserName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(260)]
    public string? Bio { get; set; }

    [StringLength(260)]
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsLocked { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();
    public ICollection<Report> CreatedReports { get; set; } = new List<Report>();
    public ICollection<Report> TargetedReports { get; set; } = new List<Report>();
    public ICollection<Report> ReviewedReports { get; set; } = new List<Report>();
    public ICollection<AdminLog> AdminLogs { get; set; } = new List<AdminLog>();
}
