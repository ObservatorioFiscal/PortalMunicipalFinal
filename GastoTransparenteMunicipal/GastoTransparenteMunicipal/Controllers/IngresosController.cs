using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using GastoTransparenteMunicipal.Models;

namespace GastoTransparenteMunicipal.Controllers
{
    public class IngresosController : BaseController
    {
        // GET: Ingresos
        public ActionResult Index()
        {
            ViewBag.Ingreso = "active";
            var municipalidad = GetCurrentIdMunicipality();
            if (municipalidad == null)
                return RedirectToAction("List", "Home");
            if (string.IsNullOrEmpty(municipalidad.Periodo))
                return RedirectToAction("List", "Home");
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.activos = new List<bool>{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };

            List<Ingreso_Ano_Visible> tiempos = db.Ingreso_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).OrderByDescending(r => r.Nombre).ToList();
            ViewBag.Anos = new SelectList(tiempos, "IdAno", "Nombre");
            ViewBag.Destacado = "hidden";
            return View();
        }

        public ActionResult JsonIngresoNivelX(string tipo, int? idNivel, int? profundidad, int year)
        {
            #region FINAL
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string FUNCIONALECONOMICO = tipo;
            string NIVEL = idNivel.ToString();
            int PADRE = profundidad.Value;
            var idMunicipality = GetCurrentIdMunicipality();
            switch (NIVEL)
            {
                case "1":
                    List<Ingreso_Nivel1> lista1 = db.Ingreso_Ano.Find(year).Ingreso_Nivel1.Where(r => r.Tipo == FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();

                    d3.Load(lista1, "0");
                    break;
                case "2":
                    List<Ingreso_Nivel2> lista2 = db.Ingreso_Nivel2.Where(r => r.IdNivel1 == PADRE && r.Tipo == FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                    if (lista2.Count < 1)
                    {
                        lista2 = db.Ingreso_Nivel2.Where(r => r.IdNivel1 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                        if (lista2.Count < 1)
                        {
                            d3.Load(lista2, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista2, "1");// 1= Hay pero en otra categoria
                        }

                    }
                    else
                    {
                        if (!db.Ingreso_Nivel2.Any(r => r.IdNivel1 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0))
                        {
                            d3.Load(lista2, "3"); // 1= Solo hay en la que eligio
                        }
                        else
                        {
                            d3.Load(lista2, "0");// 0= Hay normal
                        }
                    }
                    break;
                case "3":
                    List<Ingreso_Nivel3> lista3 = db.Ingreso_Nivel3.Where(r => r.IdNivel2 == PADRE && r.Tipo == FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                    if (lista3.Count < 1)
                    {
                        lista3 = db.Ingreso_Nivel3.Where(r => r.IdNivel2 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                        if (lista3.Count < 1)
                        {
                            d3.Load(lista3, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista3, "1");
                        }
                    }
                    else
                    {
                        if (!db.Ingreso_Nivel3.Any(r => r.IdNivel2 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0))
                        {
                            d3.Load(lista3, "3");
                        }
                        else
                        {
                            d3.Load(lista3, "0");
                        }
                    }
                    break;
                case "4":
                    List<Ingreso_Nivel4> lista4 = db.Ingreso_Nivel4.Where(r => r.IdNivel3 == PADRE && r.Tipo == FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                    if (lista4.Count < 1)
                    {
                        lista4 = db.Ingreso_Nivel4.Where(r => r.IdNivel3 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0).OrderByDescending(r => r.MontoGastado).ToList();
                        if (lista4.Count < 1)
                        {
                            d3.Load(lista4, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista4, "1");
                        }
                    }
                    else
                    {
                        if (!db.Ingreso_Nivel4.Any(r => r.IdNivel3 == PADRE && r.Tipo != FUNCIONALECONOMICO && r.MontoGastado > 0))
                        {
                            d3.Load(lista4, "3");
                        }
                        else
                        {
                            d3.Load(lista4, "0");
                        }
                    }
                    break;
            }
            string json = d3.getJson();

            return this.Content(json, "application/json");
            #endregion
        }

        public class d3Object_TreeMap
        {
            public d3Object_TreeMap()
            {
                elementos1 = new List<string>();
            }
            private List<string> elementos1;
            private string json;
            private string formatter;
            private long total=0;

            private string getCabecera()
            {
                string cabecera = @"{
                                     ""name"": ""flare"",
                                     ""children"": [";
                return cabecera;
            }
            private string getPie()
            {
                string pie = @"  ]
                                }";
                return pie;
            }
            private string createNivel1()
            {
                string childrens = "";
                for (int i = 0; i < elementos1.Count; i++)
                {
                    string aux = elementos1[i];

                    if (i + 1 == elementos1.Count)
                        aux = aux.Substring(0, aux.Length - 1);

                    childrens += aux;
                }
                return childrens;
            }

            public void setFormato(string formatter)
            {
                this.formatter = formatter;
            }
            public void addElementoNV1(string name, string size, string nivel, string tipo, string color, string id, string idRegion)
            {
                string auxElem = @"{
                             ""name"":" + @"""" + name + @"""" +
                                 @",""size"":" + @"""" + size + @"""" +
                                 @",""tipo"":" + @"""" + tipo + @"""" +
                                 @",""valueTooltip"":" + @"""" + size + @"""" +
                                 @",""nivel"":" + @"""" + nivel + @"""" +
                                 @",""id"":" + @"""" + id + @"""" +
                                 @",""idRegion"":" + @"""" + idRegion + @"""" +
                                 @",""color"":" + @"""" + color + @"""" + "},";

                elementos1.Add(auxElem);
            }
            public string getJson()
            {
                json = getCabecera() + createNivel1() + getPie();
                json = json.Remove(json.Length - 1, 1) + ", \"Format\":\"" + this.formatter + " \"}";
                json = json.Remove(json.Length - 1, 1) + ", \"TotalSuma\":\"" + total + " \"}";
                return json;
            }

            public void Load(List<Ingreso_Nivel1> d3Object, string formatter)
            {
                this.formatter = formatter;

                foreach (var nv1 in d3Object)
                {
                    total = total + nv1.MontoGastado.Value;
                    addElementoNV1222(
                        nv1.Nombre,
                        nv1.MontoGastado.ToString(),
                        nv1.MontoPresupuestado.ToString(),
                        "1",
                        nv1.Tipo,
                        "#ea9393",
                        nv1.IdNivel1.ToString(),
                        nv1.PorcentajeGastado.ToString(),
                        nv1.PorcentajePresupuestado.ToString(),
                        nv1.Descripcion);
                }
            }
            public void Load(List<Ingreso_Nivel2> d3Object, string formatter)
            {
                this.formatter = formatter;
                foreach (var nv1 in d3Object)
                {
                    total = total + nv1.MontoGastado.Value;
                    addElementoNV1222(
                        nv1.Nombre,
                        nv1.MontoGastado.ToString(),
                        nv1.MontoPresupuestado.ToString(),
                        "1",
                        nv1.Tipo,
                        "#ea9393",
                        nv1.IdNivel2.ToString(),
                        nv1.PorcentajeGastado.ToString(),
                        nv1.PorcentajePresupuestado.ToString(),
                        nv1.Descripcion
                    );
                }
            }
            public void Load(List<Ingreso_Nivel3> d3Object, string formatter)
            {
                this.formatter = formatter;
                foreach (var nv1 in d3Object)
                {
                    total = total + nv1.MontoGastado.Value;
                    addElementoNV1222(
                        nv1.Nombre,
                        nv1.MontoGastado.ToString(),
                        nv1.MontoPresupuestado.ToString(),
                        "1",
                        nv1.Tipo,
                        "#ea9393",
                        nv1.IdNivel3.ToString(),
                        nv1.PorcentajeGastado.ToString(),
                        nv1.PorcentajePresupuestado.ToString(),
                        nv1.Descripcion);
                }
            }
            public void Load(List<Ingreso_Nivel4> d3Object, string formatter)
            {
                this.formatter = formatter;
                foreach (var nv1 in d3Object)
                {
                    total = total + nv1.MontoGastado.Value;
                    addElementoNV1222(
                        nv1.Nombre,
                        nv1.MontoGastado.ToString(),
                        nv1.MontoPresupuestado.ToString(),
                        "1",
                        nv1.Tipo,
                        "#ea9393",
                        nv1.IdNivel4.ToString(),
                        nv1.PorcentajeGastado.ToString(),
                        nv1.PorcentajePresupuestado.ToString(),
                        nv1.Descripcion);
                }
            }

            public void addElementoNV1222(string name, string size, string size2, string nivel, string tipo, string color, string id, string porcentaje1, string porcentaje2, string descripcion)
            {
                long numero = long.Parse(size);
                long numero2 = long.Parse(size2);
                string valueTooltip = string.Format(new System.Globalization.CultureInfo("is-IS"), "{0:N0}", numero);
                string valueTooltip2 = string.Format(new System.Globalization.CultureInfo("is-IS"), "{0:N0}", numero2);
                string auxElem = @"{
                             ""name"":" + @"""" + name + @"""" +
                                 @",""size"":" + @"""" + size + @"""" +
                                 @",""tipo"":" + @"""" + tipo + @"""" +
                                 @",""valueTooltip1"":" + @"""" + valueTooltip + @"""" +
                                 @",""valueTooltip2"":" + @"""" + valueTooltip2 + @"""" +
                                 @",""porcentaje1"":" + @"""" + porcentaje1 + @"""" +
                                 @",""porcentaje2"":" + @"""" + porcentaje2 + @"""" +
                                 @",""descripcion"":" + @"""" + descripcion + @"""" +
                                 @",""nivel"":" + @"""" + nivel + @"""" +
                                 @",""id"":" + @"""" + id + @"""" +
                                 @",""color"":" + @"""" + color + @"""" + "},";

                elementos1.Add(auxElem);
            }

        }

    }
}
