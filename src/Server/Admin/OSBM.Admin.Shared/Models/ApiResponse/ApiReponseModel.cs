using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OSBM.Admin.Shared.Models.ApiResponse;

public class ApiReponseModel
{
    public ApiReponseModel()
    {
    }

    public ApiReponseModel(bool isSuccess, int statusCode, string? messages = null, string? stackTrace = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Messages = messages;
        StackTrace = stackTrace;
    }

    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Messages { get; set; }
    public object? Data { get; set; }
    public string? StackTrace { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
    }
}