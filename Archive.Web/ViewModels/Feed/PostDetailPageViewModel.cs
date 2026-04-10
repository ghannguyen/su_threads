using Archive.Web.ViewModels.Shared;

namespace Archive.Web.ViewModels.Feed;

public class PostDetailPageViewModel
{
    public PostCardViewModel Post { get; set; } = new();
    public List<CommentItemViewModel> Comments { get; set; } = new();
    public PostComposerViewModel QuoteComposer { get; set; } = new();
}
