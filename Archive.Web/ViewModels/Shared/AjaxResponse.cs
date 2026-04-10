namespace Archive.Web.ViewModels.Shared;

public class AjaxResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static AjaxResponse<T> Ok(T data, string message = "") => new()
    {
        Status = true,
        Message = message,
        Data = data
    };

    public static AjaxResponse<T> Fail(string message) => new()
    {
        Status = false,
        Message = message
    };
}
