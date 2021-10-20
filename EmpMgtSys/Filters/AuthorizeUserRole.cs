using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpMgtSys.Filters
{ 

    public class AuthorizeUserRoles : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {



            if (HttpContext.Current.Session["UserAdmin"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller","Home" },
                    {"Action","Login" }
                });
            }

            base.OnActionExecuted(filterContext);




        }
    }
}