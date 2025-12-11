using System;
using System.Web;
using System.Web.Mvc;

namespace Proyect.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (session == null || session["UserID"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult(401, "No autorizado");
                }
                else
                {
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Action("Login", "Auth");
                    filterContext.Result = new RedirectResult(loginUrl);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}