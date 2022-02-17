namespace Notes.WebApi.Middleware;
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError;
        string result = string.Empty;
        switch (exception)
        {
            case FluentValidation.ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/json";

        if (result == String.Empty)
            result = JsonSerializer.Serialize(new { exception.Message });

        return context.Response.WriteAsync(result);
    }
}
