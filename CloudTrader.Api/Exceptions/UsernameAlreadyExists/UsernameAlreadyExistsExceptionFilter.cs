using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudTrader.Api.Exceptions
{
    public class UsernameAlreadyExistsExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ConflictObjectResult(context.Exception.Message);
        }
    }
}
