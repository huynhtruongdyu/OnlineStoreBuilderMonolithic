using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Shared.Models.ApiResponse;

using System.Net;

namespace OSBM.Admin.API.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseApiResponse : ControllerBase
{
    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), 200)]
    public IActionResult SuccessResult(string message)
    {
        return Ok(new ApiReponseModel()
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Messages = message
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), 200)]
    public IActionResult SuccessResult(object data)
    {
        return Ok(new ApiReponseModel()
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = data
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), (int)HttpStatusCode.BadRequest)]
    public IActionResult BadResult(string message)
    {
        return BadRequest(new ApiReponseModel()
        {
            IsSuccess = false,
            Messages = message,
            StatusCode = (int)HttpStatusCode.BadRequest
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), (int)HttpStatusCode.BadRequest)]
    public IActionResult BadResult(string message, HttpStatusCode statusCode)
    {
        return BadRequest(new ApiReponseModel()
        {
            IsSuccess = false,
            StatusCode = (int)statusCode
        });
    }

    [NonAction]
    [ProducesResponseType(typeof(ApiReponseModel), (int)HttpStatusCode.BadRequest)]
    public IActionResult ErrorResult(string messages, HttpStatusCode statusCode)
    {
        return BadRequest(new ApiReponseModel()
        {
            IsSuccess = false,
            StatusCode = (int)statusCode,
            Messages = messages
        });
    }
}