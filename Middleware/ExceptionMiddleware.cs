#pragma warning disable CS8629

namespace capsaicin_events_sharp;

public class ExceptionMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) {
        try {
            await _next(httpContext);
        }
        catch (HttpRequestException ex) {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpRequestException exception) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;
        await context.Response.WriteAsync(new capsaicin_events_sharp.GlobalError() {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
}