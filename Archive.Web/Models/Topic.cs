using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class Topic
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(180)]
    public string? Description { get; set; }

    public bool IsVisible { get; set; } = true;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
