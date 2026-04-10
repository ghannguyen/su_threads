using Archive.Web.Data;
using Archive.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Archive.Web.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _dbContext;

    public ReportService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult> ReportPostAsync(int reporterId, int postId, string reason, string? details)
    {
        var postExists = await _dbContext.Posts.AnyAsync(x => x.Id == postId && !x.IsDeleted);
        if (!postExists)
        {
            return ServiceResult.Fail("Bài viết không tồn tại.");
        }

        _dbContext.Reports.Add(new Report
        {
            ReporterUserId = reporterId,
            TargetPostId = postId,
            Reason = reason.Trim(),
            Details = details?.Trim(),
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
        return ServiceResult.Ok("Đã gửi báo cáo bài viết.");
    }

    public async Task<ServiceResult> ReportUserAsync(int reporterId, int targetUserId, string reason, string? details)
    {
        var userExists = await _dbContext.Users.AnyAsync(x => x.Id == targetUserId);
        if (!userExists)
        {
            return ServiceResult.Fail("Người dùng không tồn tại.");
        }

        _dbContext.Reports.Add(new Report
        {
            ReporterUserId = reporterId,
            TargetUserId = targetUserId,
            Reason = reason.Trim(),
            Details = details?.Trim(),
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
        return ServiceResult.Ok("Đã gửi báo cáo tài khoản.");
    }
}
