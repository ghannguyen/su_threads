using Archive.Web.ViewModels.Shared;

namespace Archive.Web.ViewModels.Feed;

public class FeedPageViewModel
{
    public PostComposerViewModel Composer { get; set; } = new();
    public List<PostCardViewModel> Posts { get; set; } = new();
    public List<UserSummaryViewModel> Suggestions { get; set; } = new();
    public List<TopicSummaryViewModel> TrendingTopics { get; set; } = new();
}
