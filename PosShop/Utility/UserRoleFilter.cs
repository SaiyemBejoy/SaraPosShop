using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;

namespace PosShop.Utility
{
    public class UserRoleFilter : ActionFilterAttribute
    {
        private readonly IAuthManager _manager = new AuthManager();

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.Values.ContainsKey("Controller") ? filterContext.RequestContext.RouteData.Values["Controller"].ToString() : null;
            var action = filterContext.RequestContext.RouteData.Values.ContainsKey("Action") ? filterContext.RequestContext.RouteData.Values["Action"].ToString() : null;
            var ip = filterContext.HttpContext.Request.UserHostAddress;
            var sessionUser = (AuthModel)HttpContext.Current.Session["authentication"];
            if (sessionUser.EmployeeRole != "")// Check the Role Against the database Value
            {
                string requiredPermission = String.Format("{0}/{1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName);

                var access = await _manager.RoleWiseActionPermision(controller, action, sessionUser.EmployeeRole);
                if (access == "null")
                {
                    filterContext.HttpContext.Session.RemoveAll();
                    var urlHelper = new UrlHelper(filterContext.RequestContext);
                    var url = urlHelper.Action("Index", "Auth");
                    filterContext.Result = new RedirectResult(url);
                }

            }
        }
    }
}