using System.Net;

namespace OSBM.Admin.Shared.Models.ApiResponse;

public class ApiReponseModel
{
    public int StatusCode { get; set; }
    public string? Messages { get; set; }
    public string? StackTrace { get; set; }
    public object? Data { get; set; }
    public bool IsSuccess { get; set; }
}