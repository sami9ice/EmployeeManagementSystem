using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpMgtSys.Filters
{
    public class AuthorizeAdmin: System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter 
    {
        public ActionExecutedContext filterContext { get; private set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (HttpContext.Current.Session["Admin"] == null)
            {
                if (HttpContext.Current.Session["User"] == null)


                {

                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller","Home" },

                    {"Action","Login" }
                });

                }

                RedirectToRouteResult();

                base.OnActionExecuted(filterContext);
            }


             

           




            else if
                 (HttpContext.Current.Session["User"] == null)
                {


                if (HttpContext.Current.Session["Admin"] == null)


                {

                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                         {
                            {"Controller","Home" },

                            {"Action","Login" }
                         });




                }

                base.OnActionExecuted(filterContext);
                RedirectToRouteResult();



            }
            base.OnActionExecuted(filterContext);


        }






        private void RedirectToRouteResult()
        {
            base.OnActionExecuted(filterContext);

        }




    }

      
    

    //public class AuthorizeUserRole : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    //{
    //    public override void OnActionExecuted(ActionExecutedContext filterContext)
    //    {



    //        if (HttpContext.Current.Session["SuperAdmin"] == null)
    //        {
    //            filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
    //            {
    //                {"Controller","Account" },
    //                {"Action","Login" }
    //            });
    //        }

    //        base.OnActionExecuted(filterContext);




    //    }
    //}

}