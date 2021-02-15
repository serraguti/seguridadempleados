using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
                //DEBEMOS RECUPERAR LA INFORMACION DE DONDE HA
                //PULSADO EL USUARIO
                String action = context.RouteData.Values["action"].ToString();
                String controller = context.RouteData.Values["controller"].ToString();
                //NECESITAMOS RECUPERAR EL PROVEEDOR DE TEMPDATA
                //AL NO SER UNA CLASE CONTROLLER, NO ES NATIVO
                //PARA ENCONTRARLO, DEBEMOS RECUPERAR LA CLASE QUE HEMOS
                //PUESTO EN LAS DEPENDENCIAS DE LA APP (STARTUP)
                //RESOLVER DEPENDENCIAS DE UN SERVICIO DEL CONTENEDOR IoC
                ITempDataProvider provider = (ITempDataProvider)
                    context.HttpContext.RequestServices
                    .GetService(typeof(ITempDataProvider));
                //RECUPERAMOS EL TEMPDATA DEL PROVIDER
                var TempData = provider.LoadTempData(context.HttpContext);
                //GUARDAMOS LOS DATOS
                TempData["action"] = action;
                TempData["controller"] = controller;
                //DEBEMOS SALVAR TEMPDATA PARA QUE LLEGUE AL CONTROLLER
                provider.SaveTempData(context.HttpContext, TempData);

                //LOGIN
                context.Result = this.GetRedirectToRoute("Identity", "Login");
            }
            else
            {
                //SOLO QUEREMOS QUE EL PRESI ENTRE
                if (user.IsInRole("PRESIDENTE") == false
                    && user.IsInRole("ANALISTA") == false
                    && user.IsInRole("DIRECTOR") == false)
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
