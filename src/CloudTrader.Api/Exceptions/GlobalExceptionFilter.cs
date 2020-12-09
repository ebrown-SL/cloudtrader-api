using CloudTrader.Api.Domain.Exceptions;
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
                case ApiConnectionError _:
                    context.Result = new NotFoundResult();
                    break;

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