using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }

    public int? QuotePostId { get; set; }
    public Post? QuotePost { get; set; }

    [Required, StringLength(300)]
    public string Content { get; set; } = string.Empty;

    public bool IsHidden { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<Post> QuotedByPosts { get; set; } = new List<Post>();
    public ICollection<PostImage> Images { get; set; } = new List<PostImage>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();
    public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
    public ICollection<Report> Reports { get; set; } = new List<Report>();
}
