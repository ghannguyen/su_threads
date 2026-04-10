using Archive.Web.ViewModels.Admin;

namespace Archive.Web.Services;

public interface IAdminService
{
    Task<AdminDashboardViewModel> GetDashboardAsync();
    Task<List<AdminUserRowViewModel>> GetUsersAsync();
    Task<List<AdminPostRowViewModel>> GetPostsAsync();
    Task<List<AdminTopicRowViewModel>> GetTopicsAsync();
    Task<EditTopicViewModel?> GetTopicEditModelAsync(int id);
    Task<ServiceResult> SaveTopicAsync(EditTopicViewModel model);
    Task<ServiceResult> ToggleUserLockAsync(int adminUserId, int userId);
    Task<ServiceResult> TogglePostVisibilityAsync(int adminUserId, int postId);
}
