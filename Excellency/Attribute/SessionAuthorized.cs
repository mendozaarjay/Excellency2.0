using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Excellency
{
    public class SessionAuthorized : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home"},
                    {"action", "Login"}
                });
                return;
            }
            if (context.HttpContext.Session.GetString("UserId").Length <= 0)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home"},
                    {"action", "Login"}
                });
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
