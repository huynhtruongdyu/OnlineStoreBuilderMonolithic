using System.Net;

namespace OSBM.Admin.Shared.Models.ApiResponse;

public class ApiReponseModel
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Messages { get; set; }
    public object? Data { get; set; }
    public string? StackTrace { get; set; }
}