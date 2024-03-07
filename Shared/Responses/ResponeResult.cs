using Shared.Responses.Enums;

namespace Shared.Responses;

public class ResponseResult
{
    public ResultStatus StatusCode { get; set; }
    public object? ContentResult { get; set; }
    public string? Message { get; set; }
}
