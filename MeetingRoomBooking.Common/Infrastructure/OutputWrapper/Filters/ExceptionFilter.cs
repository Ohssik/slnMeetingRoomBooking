using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Exceptions;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models;
using System.Net;
//using ExceptionContext = Microsoft.AspNetCore.Mvc.Filters.ExceptionContext;

namespace MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Filters
{
    /// <summary>
    /// Class ValidateExceptionFilter.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncExceptionFilter" />
    public class ExceptionFilter: IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (!(context.Exception is ValidateException exception))
            {
                return Task.CompletedTask;
            }

            context.Exception = null;

            var output = new FailureResultOutputModel
            {
                Id = EvertrustAsyncContext.CorrelationId,
                Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                Status = "ValidationError",
                ApiVersion = EvertrustAsyncContext.Version
            };

            output.Errors.Add(new FailureInformation
            {
                ErrorCode = 30001,
                Message = exception.Result.Error.Message,
                Description = exception.Result.Error.Description
            });

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(output)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            return Task.CompletedTask;
        }
    }
}
