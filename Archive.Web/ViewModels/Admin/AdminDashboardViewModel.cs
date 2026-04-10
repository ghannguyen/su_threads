namespace Archive.Web.ViewModels.Admin;

public class AdminDashboardViewModel
{
    public int UserCount { get; set; }
    public int ActiveUserCount { get; set; }
    public int LockedUserCount { get; set; }
    public int PostCount { get; set; }
    public int HiddenPostCount { get; set; }
    public int CommentCount { get; set; }
    public int ReportCount { get; set; }
    public int PendingReportCount { get; set; }
}
