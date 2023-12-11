using ID.Core.Exceptions.Base;
using ISDS.ServiceExtender.Http;
using ISDS.ServiceExtender.Logging;
using System.Net;

namespace ID.Host.Infrastracture.Middlewares.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly ILoggerService _loggerService;

        public ExceptionHandlerMiddleware(RequestDelegate nextDelegate, ILoggerService loggerService)
        {
            _nextDelegate = nextDelegate ?? throw new ArgumentNullException(nameof(nextDelegate));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextDelegate.Invoke(context);
            }
            catch (BaseIDException idException)
            {
                _loggerService.Error(idException);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(ThrowHelper.HandleIDException(idException));
            }
            catch (Exception exception)
            {
                _loggerService.Fatal(exception);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(AjaxResult.Error("Произошла неизвестная ошибка на сервере"));
            }
        }
    }
}
