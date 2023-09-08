using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.Shared.Models.ApiResponse;

using System.Net;

namespace OSBM.Admin.API.Controllers.Base;

public class BaseApiResponse : ControllerBase
{
    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), StatusCodes.Status200OK)]
    public IActionResult SuccessResult(string message)
    {
        return Ok(new ApiReponseModel()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Messages = message
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), StatusCodes.Status200OK)]
    public IActionResult SuccessResult(object data)
    {
        return Ok(new ApiReponseModel()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Data = data
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), StatusCodes.Status400BadRequest)]
    public IActionResult BadResult(string message)
    {
        return BadRequest(new ApiReponseModel()
        {
            IsSuccess = false,
            Messages = message,
            StatusCode = StatusCodes.Status400BadRequest
        });
    }
}