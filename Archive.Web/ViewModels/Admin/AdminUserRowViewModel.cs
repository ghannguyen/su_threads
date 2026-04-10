namespace Archive.Web.ViewModels.Admin;

public class AdminUserRowViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public int PostCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
