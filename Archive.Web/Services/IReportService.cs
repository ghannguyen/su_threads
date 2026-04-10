namespace Archive.Web.Services;

public interface IReportService
{
    Task<ServiceResult> ReportPostAsync(int reporterId, int postId, string reason, string? details);
    Task<ServiceResult> ReportUserAsync(int reporterId, int targetUserId, string reason, string? details);
}
