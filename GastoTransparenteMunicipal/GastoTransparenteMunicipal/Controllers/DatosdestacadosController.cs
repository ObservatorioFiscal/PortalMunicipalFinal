using System;
using System.Collections.Generic;
using GastoTransparenteMunicipal.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Newtonsoft.Json;

namespace GastoTransparenteMunicipal.Controllers
{
    public class DatosdestacadosController : BaseController
    {                
        // GET: Datosdestacados
        public ActionResult Sueldos()
        {
            var municipalidad = GetCurrentIdMunicipality();
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List");
            ViewBag.logo = municipalidad.DireccionWeb+".png";
            List<Personal_Ano_Visible> tiempos = db.Personal_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).OrderByDescending(r => r.Nombre).ToList();

            ViewBag.Anos = new SelectList(tiempos, "IdAno", "Nombre");
            ViewBag.Sueldos = "active";
            ViewBag.activos = new List<bool>{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            ViewBag.cementerio = municipalidad.Cementerio;
            return View();
        }

        // GET: Datosdestacados
        public ActionResult Proveedores()
        {
            var municipalidad = GetCurrentIdMunicipality();
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List");
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            List<Proveedor_Ano_Visible> tiempos = db.Proveedor_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).OrderByDescending(r=>r.Nombre).ToList();

            ViewBag.Anos = new SelectList(tiempos, "IdAno", "Nombre");
            ViewBag.Proveedores = "active";
            ViewBag.activos = new List<bool>{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            ViewBag.cementerio = municipalidad.Cementerio;
            return View();
        }

        // GET: Datosdestacados
        public ActionResult Subsidios()
        {
            var municipalidad = GetCurrentIdMunicipality();
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List");
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            List<Subsidio_Ano_Visible> tiempos = db.Subsidio_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).OrderByDescending(r => r.Nombre).ToList();

            ViewBag.Anos = new SelectList(tiempos, "IdAno", "Nombre");
            ViewBag.Subsidios = "active";
            ViewBag.activos = new List<bool>{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            return View();
        }

        // GET: Datosdestacados
        public ActionResult Corporaciones()
        {
            var municipalidad = GetCurrentIdMunicipality();
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List");
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            List<Corporacion_Ano_Visible> tiempos = db.Corporacion_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).OrderByDescending(r => r.Nombre).ToList();

            ViewBag.Anos = new SelectList(tiempos, "IdAno", "Nombre");
            ViewBag.Corporaciones = "active";
            ViewBag.activos = new List<bool>{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            return View();
        }

    }
}
