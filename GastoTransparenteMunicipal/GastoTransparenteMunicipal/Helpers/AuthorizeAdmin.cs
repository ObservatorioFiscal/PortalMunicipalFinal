using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GastoTransparenteMunicipal.Helpers
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var parametros = httpContext.Request.RequestContext.RouteData;
            var municipalidad = parametros.Values["municipality"] as string;
            municipalidad = municipalidad.ToLower();

            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // Usuario no autenticado
                return false;
            }

            var user = httpContext.User;
            if (!user.IsInRole("admin"))
            {
                //Usuario administrador
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {        
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { municipality = "admin", controller = "Account", action = "Login"}));
        }
    }
}

