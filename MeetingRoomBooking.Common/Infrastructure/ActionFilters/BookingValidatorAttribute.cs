using FluentValidation;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;

namespace MeetingRoomBooking.Common.Infrastructure.ActionFilters
{
    public class BookingValidatorAttribute:ActionFilterAttribute
    {
        private readonly Type _validatorType;
        public BookingValidatorAttribute(Type validatorType) 
        {
            _validatorType = validatorType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next )
        {            
            var parameters=actionContext.ActionArguments;
            if(parameters.Count<=0) 
            {
                await base.OnActionExecutionAsync(actionContext,next);
            }

            var parameter = parameters.FirstOrDefault();
            if (parameter.Value == null)
            {
                actionContext.Result = new BadRequestObjectResult("未輸入 Parameter");
            }

            var validator = Activator.CreateInstance(this._validatorType) as IValidator;
            var validationContext = new ValidationContext<object>(parameter.Value);
            var validationResult = await validator.ValidateAsync(validationContext);

            if (validationResult.IsValid.Equals(false))
            {
                //var error = validationResult.Errors.FirstOrDefault();

                var failureOutputModel = new FailureResultOutputModel
                {
                    Id = EvertrustAsyncContext.CorrelationId,
                    ApiVersion = EvertrustAsyncContext.Version,
                    Method = $"{actionContext.HttpContext.Request.Path}.{actionContext.HttpContext.Request.Method}",
                    Status = "VaildationError",                    
                    Errors = validationResult.Errors.Select
                    (
                        item => new FailureInformation
                        {
                            ErrorCode = 30001,
                            PropertyName = item.PropertyName,
                            Message = item.ErrorMessage
                        }
                    ).ToList()

                };                

                actionContext.Result = new BadRequestObjectResult(failureOutputModel.Errors);

                //throw new ValidateException(new RequestValidateResult()
                //{
                //    ParameterName = error.PropertyName,
                //    ParameterValue = error.AttemptedValue,
                //    Error = new ErrorMessage()
                //    {
                //        Code = "30001",
                //        Message = error.ErrorMessage,
                //        Description = error.ErrorMessage
                //    }
                //});
            }

            await base.OnActionExecutionAsync(actionContext, next);
        }
    }
}
