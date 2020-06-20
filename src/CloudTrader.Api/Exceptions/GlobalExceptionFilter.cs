using CloudTrader.Api.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudTrader.Api.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case UsernameAlreadyExistsException exception:
                    context.Result = new ConflictObjectResult(exception.Message);
                    break;
                case UnauthorizedException exception:
                    context.Result = new UnauthorizedObjectResult(exception.Message);
                    break;
                default:
                    context.Result = new StatusCodeResult(500);
                    break;
            }
        }
    }
}
