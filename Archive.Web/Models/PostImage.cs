using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class PostImage
{
    public int Id { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }

    [Required, StringLength(260)]
    public string ImageUrl { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}
