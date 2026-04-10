using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Archive.Web.ViewModels.Feed;

public class PostComposerViewModel
{
    [Required(ErrorMessage = "Bài viết không được để trống.")]
    [StringLength(300, ErrorMessage = "Bài viết tối đa 300 ký tự.")]
    public string Content { get; set; } = string.Empty;

    public int? TopicId { get; set; }

    public IFormFile? ImageFile { get; set; }

    public int? QuotePostId { get; set; }

    public List<SelectListItem> TopicOptions { get; set; } = new();
}
