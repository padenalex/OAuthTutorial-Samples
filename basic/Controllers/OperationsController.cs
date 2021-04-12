using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers
{
    public class OperationsController : Controller
    {
        
    }

    public class CookieJarAuthorizationHandler 
        : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement)
        {
            throw new System.NotImplementedException();
        }
    }
}