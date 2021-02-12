using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadEmpleados.Filters
{
    public class AuthorizeEmpleadosAttribute : AuthorizeAttribute
        , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                //LOGIN
                context.Result = this.GetRedirectToRoute("Identity", "Login");
            }
            else
            {
                //SOLO QUEREMOS QUE EL PRESI ENTRE
                if (user.IsInRole("PRESIDENTE") == false)
                {
                    context.Result =
                        this.GetRedirectToRoute("Identity", "AccesoDenegado");
                }
            }
        }
        private RedirectToRouteResult 
            GetRedirectToRoute(String controller
            , String action)
        {
            RouteValueDictionary ruta =
                new RouteValueDictionary(new { 
                    controller = controller,
                    action = action
                });
            RedirectToRouteResult redirect =
                new RedirectToRouteResult(ruta);
            return redirect;
        }
    }
}
