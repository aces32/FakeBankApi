using Microsoft.AspNetCore.Builder;

namespace FakeBank.BankAPI.Api.MiddleWare
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
