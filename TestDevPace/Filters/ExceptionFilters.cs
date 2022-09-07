using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestDevPace.Business.Infrastructure.Validation;

namespace TestDevPace.Filters
{
    public class ExceptionFilters : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                AuthorizeException => new UnauthorizedObjectResult(context.Exception.Message),
                IncorrectModelException => new BadRequestObjectResult(context.Exception.Message),
                Exception => new BadRequestObjectResult(context.Exception.Message)
            };
        }
    }
}
