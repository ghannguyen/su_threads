using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }

    [Required, StringLength(220)]
    public string Content { get; set; } = string.Empty;

    public bool IsHidden { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
