namespace Archive.Web.Models;

public class Follow
{
    public int Id { get; set; }

    public int FollowerId { get; set; }
    public AppUser? Follower { get; set; }

    public int FollowingId { get; set; }
    public AppUser? Following { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
