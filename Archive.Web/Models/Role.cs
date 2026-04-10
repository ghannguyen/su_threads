using System.ComponentModel.DataAnnotations;

namespace Archive.Web.Models;

public class Role
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(160)]
    public string Description { get; set; } = string.Empty;

    public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
}
