using ID.Core.Exceptions.Base;
using System.Net;

namespace ID.Host.Infrastracture.Middlewares.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate ?? throw new ArgumentNullException(nameof(nextDelegate));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextDelegate.Invoke(context);
            }
            catch (BaseIDException idException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(ThrowHelper.HandleIDException(idException));
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(AjaxResult.Error("Произошла неизвестная ошибка на сервере"));
            }
        }
    }
}
