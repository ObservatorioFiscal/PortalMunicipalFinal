using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace GastoTransparenteMunicipal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string routeNotMunicipality = "~/list/Home/Error";
        private const string routeInactivityMunicipality = "~/list/Home/List";
        private const string routeServerStatus = "~/list/Home/ServiceStatus";
        private const string baseRoute = "list";
        private const string baseAdminRoute = "admin";
        private List<string> allowRoute = new List<string>() { "/list/Home/List", "/list/Home/ServiceStatus" };

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperConfig.Mapping();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            GastoTransparenteMunicipalEntities db = new GastoTransparenteMunicipalEntities();
            try
            {
                string currentPath = Request.Path.ToLower();
                HttpSessionState curSession = HttpContext.Current.Session;
                var parameters = currentPath.Split('/');
                bool allow = allowRoute.Any(r => r.ToLower() == currentPath) || currentPath.Contains("__browserlink");

                if (!allow)
                {
                    if (parameters.Count() >= 1 && parameters[1] != "")
                    {
                        var municipalityName = parameters[1].ToLower();
                        var municipality = db.Municipalidad.SingleOrDefault(r => r.DireccionWeb.ToLower() == municipalityName);
                        if (municipalityName != baseAdminRoute && !routeNotMunicipality.Contains(currentPath) && !routeNotMunicipality.Contains(routeInactivityMunicipality))
                        {
                            if (municipality == null && municipalityName != baseRoute)
                            {
                                Redirect(ref curSession, routeInactivityMunicipality);
                            }
                            else
                            {
                                if (municipality == null || !municipality.Activa)
                                {
                                    Redirect(ref curSession, routeInactivityMunicipality);
                                }
                            }
                        }
                    }
                    else
                    {
                        Redirect(ref curSession, routeInactivityMunicipality);
                    }
                }
            }
            catch(Exception ex)
            {
                var d = ex;
            }
        } 

        public void Redirect(ref HttpSessionState curSession, string path)
        {
            curSession.Clear();
            HttpContext.Current.Server.ClearError();
            HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
            HttpContext.Current.Response.Redirect(path, false);
            HttpContext.Current.Response.Flush(); 
            HttpContext.Current.Response.SuppressContent = true;  
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}

