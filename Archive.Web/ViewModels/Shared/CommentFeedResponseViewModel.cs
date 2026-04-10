namespace Archive.Web.ViewModels.Shared;

public class CommentFeedResponseViewModel
{
    public int Count { get; set; }
    public List<CommentItemViewModel> Comments { get; set; } = new();
}
