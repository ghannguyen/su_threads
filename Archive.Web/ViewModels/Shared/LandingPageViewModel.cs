namespace Archive.Web.ViewModels.Shared;

public class LandingPageViewModel
{
    public int UserCount { get; set; }
    public int PostCount { get; set; }
    public int TopicCount { get; set; }
    public List<PostCardViewModel> FeaturedPosts { get; set; } = new();
}
