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
            string routeNotMunicipality = "~/list/Home/Error";
            string routeInactivityMunicipality = "~/list/Home/List";
            string baseRoute = "list";
            string baseAdminRoute = "admin";

            try
            {
                string currentPath = Request.Path.ToLower();
                HttpSessionState curSession = HttpContext.Current.Session;
                var parameters = currentPath.Split('/');

                if (parameters.Count() < 1 || parameters[1] == "")
                {
                    curSession.Clear();
                    HttpContext.Current.Server.ClearError();
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                    HttpContext.Current.Response.Redirect(routeInactivityMunicipality, true);
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                    HttpContext.Current.Response.End();
                }
                else
                {
                    var municipalityName = parameters[1].ToLower();
                    var municipality = db.Municipalidad.SingleOrDefault(r => r.DireccionWeb.ToLower() == municipalityName);
                    if (municipalityName != baseAdminRoute && !routeNotMunicipality.Contains(currentPath) && !routeNotMunicipality.Contains(routeInactivityMunicipality))
                    {
                        if (municipality == null && municipalityName != baseRoute)
                        {
                            curSession.Clear();
                            HttpContext.Current.Server.ClearError();
                            HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                            HttpContext.Current.Response.Redirect(routeNotMunicipality, true);
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                            HttpContext.Current.Response.End();
                        }
                        else
                        {
                            if (!municipality.Activa)
                            {
                                curSession.Clear();
                                HttpContext.Current.Server.ClearError();
                                HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                                HttpContext.Current.Response.Redirect(routeInactivityMunicipality, true);
                                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        } 
    }
}

