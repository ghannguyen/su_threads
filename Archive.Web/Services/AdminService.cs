using Archive.Web.Data;
using Archive.Web.Extensions;
using Archive.Web.Models;
using Archive.Web.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace Archive.Web.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _dbContext;

    public AdminService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdminDashboardViewModel> GetDashboardAsync()
    {
        return new AdminDashboardViewModel
        {
            UserCount = await _dbContext.Users.CountAsync(),
            ActiveUserCount = await _dbContext.Users.CountAsync(x => x.IsActive && !x.IsLocked),
            LockedUserCount = await _dbContext.Users.CountAsync(x => x.IsLocked),
            PostCount = await _dbContext.Posts.CountAsync(x => !x.IsDeleted),
            HiddenPostCount = await _dbContext.Posts.CountAsync(x => x.IsHidden),
            CommentCount = await _dbContext.Comments.CountAsync(x => !x.IsHidden),
            ReportCount = await _dbContext.Reports.CountAsync(),
            PendingReportCount = await _dbContext.Reports.CountAsync(x => x.Status == Models.Enums.ReportStatus.Pending)
        };
    }

    public async Task<List<AdminUserRowViewModel>> GetUsersAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AdminUserRowViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                DisplayName = x.DisplayName,
                Email = x.Email,
                RoleName = x.Role!.Name,
                IsActive = x.IsActive,
                IsLocked = x.IsLocked,
                PostCount = x.Posts.Count(p => !p.IsDeleted),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<AdminPostRowViewModel>> GetPostsAsync()
    {
        return await _dbContext.Posts
            .AsNoTracking()
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AdminPostRowViewModel
            {
                Id = x.Id,
                DisplayName = x.User!.DisplayName,
                UserName = x.User!.UserName,
                Content = x.Content,
                IsHidden = x.IsHidden,
                LikeCount = x.Likes.Count,
                CommentCount = x.Comments.Count(c => !c.IsHidden),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<AdminTopicRowViewModel>> GetTopicsAsync()
    {
        return await _dbContext.Topics
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new AdminTopicRowViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                IsVisible = x.IsVisible,
                PostCount = x.Posts.Count(p => !p.IsDeleted)
            })
            .ToListAsync();
    }

    public async Task<EditTopicViewModel?> GetTopicEditModelAsync(int id)
    {
        var topic = await _dbContext.Topics
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (topic is null)
        {
            return null;
        }

        return new EditTopicViewModel
        {
            Id = topic.Id,
            Name = topic.Name,
            Slug = topic.Slug,
            Description = topic.Description,
            IsVisible = topic.IsVisible
        };
    }

    public async Task<ServiceResult> SaveTopicAsync(EditTopicViewModel model)
    {
        var slug = SlugHelper.ToSlug(model.Slug);
        var duplicate = await _dbContext.Topics
            .AnyAsync(x => x.Slug == slug && x.Id != (model.Id ?? 0));

        if (duplicate)
        {
            return ServiceResult.Fail("Slug chủ đề đã tồn tại.");
        }

        Topic topic;
        if (model.Id.HasValue)
        {
            topic = await _dbContext.Topics.FirstAsync(x => x.Id == model.Id.Value);
        }
        else
        {
            topic = new Topic();
            _dbContext.Topics.Add(topic);
        }

        topic.Name = model.Name.Trim();
        topic.Slug = slug;
        topic.Description = model.Description?.Trim();
        topic.IsVisible = model.IsVisible;

        await _dbContext.SaveChangesAsync();
        return ServiceResult.Ok("Đã lưu chủ đề.");
    }

    public async Task<ServiceResult> ToggleUserLockAsync(int adminUserId, int userId)
    {
        if (adminUserId == userId)
        {
            return ServiceResult.Fail("Admin không thể tự khóa chính mình.");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
        {
            return ServiceResult.Fail("Không tìm thấy người dùng.");
        }

        user.IsLocked = !user.IsLocked;

        _dbContext.AdminLogs.Add(new AdminLog
        {
            AdminUserId = adminUserId,
            Action = user.IsLocked ? "Lock User" : "Unlock User",
            EntityName = "User",
            EntityId = user.Id,
            Note = user.Email
        });

        await _dbContext.SaveChangesAsync();
        return ServiceResult.Ok(user.IsLocked ? "Đã khóa người dùng." : "Đã mở khóa người dùng.");
    }

    public async Task<ServiceResult> TogglePostVisibilityAsync(int adminUserId, int postId)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == postId && !x.IsDeleted);
        if (post is null)
        {
            return ServiceResult.Fail("Không tìm thấy bài viết.");
        }

        post.IsHidden = !post.IsHidden;

        _dbContext.AdminLogs.Add(new AdminLog
        {
            AdminUserId = adminUserId,
            Action = post.IsHidden ? "Hide Post" : "Show Post",
            EntityName = "Post",
            EntityId = post.Id,
            Note = post.Content.Length > 120 ? post.Content[..120] : post.Content
        });

        await _dbContext.SaveChangesAsync();
        return ServiceResult.Ok(post.IsHidden ? "Đã ẩn bài viết." : "Đã mở hiển thị bài viết.");
    }
}
