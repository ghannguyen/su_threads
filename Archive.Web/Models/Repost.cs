namespace Archive.Web.Models;

public class Repost
{
    public int Id { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
