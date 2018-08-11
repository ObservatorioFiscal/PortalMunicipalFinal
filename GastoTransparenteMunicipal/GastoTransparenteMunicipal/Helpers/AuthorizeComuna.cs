using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GastoTransparenteMunicipal.Helpers
{
    public class AuthorizeComuna : AuthorizeAttribute
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
            if (user.IsInRole("admin"))
            {
                //Usuario administrador
                return false;
            }

            if (!user.IsInRole(municipalidad))
            {
                //Usuario distinta municipalidad
                return false;
            }
            else
            {
                return true;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var Request = filterContext.HttpContext.Request;
            var parametros = filterContext.HttpContext.Request.RequestContext.RouteData;
            var municipalidad = parametros.Values["municipality"] as string;

            if (string.IsNullOrEmpty(municipalidad))
                municipalidad = "vitacura";

            municipalidad = municipalidad.ToUpper();

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { municipality = municipalidad, controller = "Account", action = "Login"}));
        }
    }
}

