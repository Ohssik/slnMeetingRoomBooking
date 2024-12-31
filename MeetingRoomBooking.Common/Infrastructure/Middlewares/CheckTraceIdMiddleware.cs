using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MeetingRoomBooking.Common.Infrastructure.Middlewares
{
    public static class CheckTraceIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckTraceId(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckTraceIdMiddleware>();
        }
    }
    public class CheckTraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckTraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.Request.Cookies.ContainsKey("TraceId"))
            {
                context.Response.Cookies.Append("TraceId", Guid.NewGuid().ToString());
            }

            await _next(context);
        }
        
    }
}
