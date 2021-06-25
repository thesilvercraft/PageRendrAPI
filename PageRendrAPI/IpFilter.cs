using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace PageRendrAPI
{
    public class ClientIPAddressFilterAttribute : ActionFilterAttribute
    { 
         public override void OnActionExecuting(ActionExecutingContext context)
         {
             var clientIPAddress = context.HttpContext.Connection.RemoteIpAddress;
             if (clientIPAddress == Program.externalIp)
             {
             context.Result = new UnauthorizedResult();
             }
         }
    }
}
