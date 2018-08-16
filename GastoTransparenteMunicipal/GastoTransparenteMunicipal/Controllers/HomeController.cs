using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastoTransparenteMunicipal.Controllers
{
    public class HomeController : BaseController
    {
        //GET: Pagina de Inicio
        public ActionResult Index()
        {
            var municipalidad = GetCurrentIdMunicipality();
            if (municipalidad == null)
                return RedirectToAction("List");
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List");
            if (municipalidad != null)
            {
                ViewBag.activos = new List<bool>
                {
                    municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
                };
                ViewBag.logo = municipalidad.DireccionWeb + ".png";
                ViewBag.Inicio = "active";
                ViewBag.Destacado = "hidden";
                ViewBag.texto1 = municipalidad.Periodo;
                ViewBag.texto2 = municipalidad.TotalGastado;
                ViewBag.texto3 = municipalidad.TotalPresupuestado;
                return View();
            }
            else
            {
                return RedirectToAction("NoExiste");
            }
            
        }

        //
        public ActionResult About()
        {
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.activos = new List<bool>
            {
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.nombre = municipalidad.Nombre;
            var municipios = db.Municipalidad.Where(r => r.Activa == true).ToList();
            return View(municipios);
        }

        //GET: Cuando existe un error.
        public ActionResult Error()
        {
            return View();
        }

        //GET: Cuando no encuentra la url
        public ActionResult Error404()
        {
            return View();
        }

        //GET: Vista comodin de informacion.
        public ActionResult List()
        {
            ViewBag.Destacado = "hidden";
            var municipalidad = GetCurrentIdMunicipality();
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.administracion = true;
                if (!this.User.IsInRole("admin"))
                {
                    ViewBag.admimuni = true;
                    ViewBag.logo = (municipalidad != null) ? municipalidad.DireccionWeb + ".png" : "municipio.png";
                }
            }
            else
            {
                if (municipalidad != null)
                {
                    ViewBag.activos = new List<bool>
                    {
                        municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
                    };
                    var municipios = db.Municipalidad.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).ToList();
                    ViewBag.logo = municipalidad.DireccionWeb + ".png";
                    return View(municipios);
                }
                else
                {
                    ViewBag.activos = new List<bool>
                    {
                        false,false,false,false
                    };
                    var municipios = db.Municipalidad.Where(r => r.Activa == true).ToList();
                    ViewBag.logo = "municipio.png";
                    return View(municipios);
                }

            }

            var listaMunicipios = db.Municipalidad.Where(r => r.Activa == true).ToList();
            return View(listaMunicipios);








        }
        
        //Descarga de Dataset de las visualizaciones.
        public JsonResult Descarga(int year, string origenData)
        {
            string aux = "";
            switch (origenData)
            {
                case "ingresos":
                    aux = db.Ingreso_Ano.Find(year).DataFilePath;
                    break;
                case "gastos":
                    aux = db.Gasto_Ano.Find(year).DataFilePath;
                    break;
                case "corporaciones":
                    aux = db.Corporacion_Ano.Find(year).DataFilePath;
                    break;
                case "subsidios":
                    aux = db.Subsidio_Ano.Find(year).DataFilePath;
                    break;
                case "proveedores":
                    aux = db.Proveedor_Ano.Find(year).DataFilePath;
                    break;
                case "sueldos":
                    aux = db.Personal_Ano.Find(year).DataFilePath;
                    break;
            }
            return Json(aux, JsonRequestBehavior.AllowGet);
            //return this.Content(aux, "application/json");
        }
    }
}