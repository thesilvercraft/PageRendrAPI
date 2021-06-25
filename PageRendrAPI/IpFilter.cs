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
             if (clientIPAddress == Program.externalIp)
             {
                Debug.WriteLine(clientIPAddress);
                Debug.WriteLine(Program.externalIp);
                context.Result = new UnauthorizedResult();
             }
         }
    }
}
