using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudTrader.Api.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(UsernameAlreadyExistsException))
            {
                context.Result = new ConflictObjectResult(context.Exception.Message);
                return;
            }

            if (exceptionType == typeof(UserNotFoundException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                return;
            }

            context.Result = new StatusCodeResult(500);
        }
    }
}
