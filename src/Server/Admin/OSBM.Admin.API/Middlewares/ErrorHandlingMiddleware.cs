namespace OSBM.Admin.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
}