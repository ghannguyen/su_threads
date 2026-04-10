using Archive.Web.Extensions;
using Archive.Web.Services;
using Archive.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archive.Web.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IProfileService _profileService;
    private readonly ISearchService _searchService;
    private readonly IFeedService _feedService;

    public ApiController(
        IPostService postService,
        IProfileService profileService,
        ISearchService searchService,
        IFeedService feedService)
    {
        _postService = postService;
        _profileService = profileService;
        _searchService = searchService;
        _feedService = feedService;
    }

    [HttpPost("like/{postId:int}")]
    public async Task<IActionResult> ToggleLike(int postId)
    {
        var userId = User.GetUserId()!.Value;
        var result = await _postService.ToggleLikeAsync(postId, userId);
        return Ok(result.Success
            ? AjaxResponse<ToggleStateViewModel>.Ok(result.Data!, result.Message)
            : AjaxResponse<ToggleStateViewModel>.Fail(result.Message));
    }

    [HttpPost("repost/{postId:int}")]
    public async Task<IActionResult> ToggleRepost(int postId)
    {
        var userId = User.GetUserId()!.Value;
        var result = await _postService.ToggleRepostAsync(postId, userId);
        return Ok(result.Success
            ? AjaxResponse<ToggleStateViewModel>.Ok(result.Data!, result.Message)
            : AjaxResponse<ToggleStateViewModel>.Fail(result.Message));
    }

    [HttpPost("follow/{targetUserId:int}")]
    public async Task<IActionResult> ToggleFollow(int targetUserId)
    {
        var userId = User.GetUserId()!.Value;
        var result = await _profileService.ToggleFollowAsync(userId, targetUserId);
        return Ok(result.Success
            ? AjaxResponse<FollowStateViewModel>.Ok(result.Data!, result.Message)
            : AjaxResponse<FollowStateViewModel>.Fail(result.Message));
    }

    [HttpGet("comments/{postId:int}")]
    public async Task<IActionResult> GetComments(int postId)
    {
        var result = await _postService.GetCommentsAsync(postId);
        return Ok(result.Success
            ? AjaxResponse<CommentFeedResponseViewModel>.Ok(result.Data!, result.Message)
            : AjaxResponse<CommentFeedResponseViewModel>.Fail(result.Message));
    }

    [HttpPost("comments/{postId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int postId, [FromForm] string content, [FromForm] int? parentCommentId)
    {
        var userId = User.GetUserId()!.Value;
        var result = await _postService.AddCommentAsync(postId, userId, content, parentCommentId);
        return Ok(result.Success
            ? AjaxResponse<CommentFeedResponseViewModel>.Ok(result.Data!, result.Message)
            : AjaxResponse<CommentFeedResponseViewModel>.Fail(result.Message));
    }

    [HttpGet("suggestions")]
    [AllowAnonymous]
    public async Task<IActionResult> Suggestions(string? q)
    {
        var results = await _searchService.GetSuggestionsAsync(q);
        return Ok(AjaxResponse<List<SearchSuggestionItemViewModel>>.Ok(results));
    }

    [HttpGet("feed/load-more")]
    public async Task<IActionResult> LoadMore(int skip = 0, int take = 5)
    {
        var userId = User.GetUserId()!.Value;
        var posts = await _feedService.LoadMorePostsAsync(userId, skip, take);
        return Ok(AjaxResponse<List<PostCardViewModel>>.Ok(posts));
    }
}
