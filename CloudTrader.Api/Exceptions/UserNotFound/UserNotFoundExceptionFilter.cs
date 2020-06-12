using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudTrader.Api.Exceptions
{
    public class UserNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
        }
    }
}
