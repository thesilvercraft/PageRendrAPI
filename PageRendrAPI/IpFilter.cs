using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace PageRendrAPI
{
    public class ClientIPAddressFilterAttribute : ActionFilterAttribute
    { 
         public override void OnActionExecuting(ActionExecutingContext context)
         {
             var clientIPAddress = context.HttpContext.Connection.RemoteIpAddress;
             Debug.WriteLine(clientIPAddress);
             Debug.WriteLine(Program.externalIp);
             if (clientIPAddress == Program.externalIp)
             {
                context.Result = new UnauthorizedResult();
             }
         }
    }
}
