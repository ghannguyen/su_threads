namespace Archive.Web.ViewModels.Admin;

public class AdminTopicRowViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsVisible { get; set; }
    public int PostCount { get; set; }
}
