namespace Archive.Web.ViewModels.Shared;

public class PostCardViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? TopicName { get; set; }
    public string? TopicSlug { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsHidden { get; set; }
    public bool IsOwnedByCurrentUser { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsRepostedByCurrentUser { get; set; }
    public bool IsQuotedPost { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int RepostCount { get; set; }
    public List<string> Hashtags { get; set; } = new();
    public PostCardViewModel? QuotedPost { get; set; }
}
