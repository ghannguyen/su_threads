using System.ComponentModel.DataAnnotations;

namespace Archive.Web.ViewModels.Admin;

public class EditTopicViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Tên chủ đề là bắt buộc.")]
    [StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug là bắt buộc.")]
    [StringLength(80)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(180)]
    public string? Description { get; set; }

    public bool IsVisible { get; set; } = true;
}
