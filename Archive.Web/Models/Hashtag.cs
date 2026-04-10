using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class Hashtag
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string Slug { get; set; } = string.Empty;

    public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
}
