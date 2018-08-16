using Core;
using GastoTransparenteMunicipal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastoTransparenteMunicipal.Controllers
{
    public class FeaturedDataController : BaseController
    {
        //****************SUBSIDIOS***************//
        //****************************************//
        #region SUBSIDIOS

        // Subsidios segun tipo de organizaciones
        [HttpPost]
        public string SubsidyAjaxNivel1(int year)
        {
            var idMunicipality = GetCurrentIdMunicipality();
            SubsidioModel subsidioModel = new SubsidioModel();
            subsidioModel.Load(db, year);
            return subsidioModel.JsonSubsidio;
        }

        // buscador de Beneficiarios
        public ActionResult SubsidyChartNivel2(int year, int orden)
        {
            var idMunicipality = GetCurrentIdMunicipality();
            SubsidioModel subsidioModel = new SubsidioModel();
            var lista = db.Subsidio_Nivel2.Where(r => r.IdAno == year).ToList();

            switch (orden)
            {
                case 1://Nombre ascendente
                    lista = lista.OrderByDescending(r => r.Nombre).ToList();
                    break;
                case 2://Nombre decendente
                    lista = lista.OrderBy(r => r.Nombre).ToList();
                    break;
                case 3://Monto ascendente
                    lista = lista.OrderByDescending(r => r.Monto).ToList();
                    break;
                default://Monto decendente o 4
                    lista = lista.OrderBy(r => r.Monto).ToList();
                    break;

            }
            return this.Json(lista.Select(r =>
                        new
                        {
                            IdNivel2 = r.IdNivel2,
                            Nombre = r.Nombre,
                            Categoria = r.Subsidio_Nivel1.Nombre,
                            Monto = r.Monto
                        }), JsonRequestBehavior.AllowGet);
        }

        // Detalles de benficios de cada organizacion
        public string SubsidyChartNivel3(int IdNivel2)
        {
            SubsidioModel subsidioModel = new SubsidioModel();
            subsidioModel.Load_Nivel3(db, IdNivel2);
            var json = JsonConvert.SerializeObject(subsidioModel.Subsidio_Nivel3);
            return json;
        }

        #endregion

        //****************PROVEEDORES*************//
        //****************************************//
        #region PROVEEDORES
        
        // Top 20 proveedores mas importantes
        public string Providers(int year, int origenData)
        {
            var idMunicipality = GetCurrentIdMunicipality();
            var takeElements = 20;
            ProveedorModel proveedorModel = new ProveedorModel();
            proveedorModel.WordCloud(db, takeElements, year, origenData);
            return proveedorModel.JsonProveedor;
        }

        // Listado de proveedores
        public string ProvidersTable(int year, int origenData, int orden)
        {
            ProveedorModel proveedorModel = new ProveedorModel();
            proveedorModel.ListaCompleta(db, year, origenData, orden);
            return proveedorModel.JsonTabla;
        }

        // Orden de compra de un proveedor en especifico
        public string ProvidersDetalle(int IdNivel1, int origenData)
        {
            ProveedorModel proveedorModel = new ProveedorModel();
            proveedorModel.ListaDetalle(db, IdNivel1, origenData);

            return proveedorModel.JsonDetalle;
        }
        #endregion
    
        //****************CORPORACION*************//
        //****************************************//
        #region CORPORACIONES
        public string CorporationAjax(int year)
        {
            CorporacionModel corporacion = new CorporacionModel();
            corporacion.Load(db, year);
            return corporacion.JsonCorporacion_Nivel1;
        }
        #endregion

        //****************PERSONAL****************//
        //****************************************//
        #region Remuneraciones
        //Listado de las remuneraciones por las distintas categorias.
        public ActionResult PersonalSalary(int year, int origenData)
        {
            var idMunicipality = GetCurrentIdMunicipality();
            Personal_Ano personal_Ano = db.Personal_Ano.Find(year);
            switch (origenData)
            {
                case OrigenData.Adm:
                    return this.Json(personal_Ano.Personal_Adm_Nivel1.OrderBy(r => r.Nombre).Select(r =>
                                      new
                                      {
                                          Item = r.CodTipo,
                                          Lista = r.Personal_Adm_Nivel2.OrderBy(l => l.Nombre).Select(l =>
                                          new
                                          {
                                              Nombre = l.Nombre,
                                              CMujer = l.CantidadMujer,
                                              CHombre = l.CantidadHombre,
                                              MMujer = l.MontoMujer,
                                              MHombre = l.MontoHombre
                                          })
                                      }), JsonRequestBehavior.AllowGet);
                case OrigenData.Salud:
                    return this.Json(personal_Ano.Personal_Salud_Nivel1.OrderBy(r => r.Nombre).Select(r =>
                                    new
                                    {
                                        Item = r.CodTipo,
                                        Lista = r.Personal_Salud_Nivel2.OrderBy(l => l.Nombre).Select(l =>
                                        new
                                        {
                                            Nombre = l.Nombre,
                                            CMujer = l.CantidadMujer,
                                            CHombre = l.CantidadHombre,
                                            MMujer = l.MontoMujer,
                                            MHombre = l.MontoHombre
                                        })
                                    }), JsonRequestBehavior.AllowGet);
                case OrigenData.Educacion:
                    return this.Json(personal_Ano.Personal_Educacion_Nivel1.OrderBy(r => r.Nombre).Select(r =>
                                    new
                                    {
                                        Item = r.CodTipo,
                                        Lista = r.Personal_Educacion_Nivel2.OrderBy(l => l.Nombre).Select(l =>
                                        new
                                        {
                                            Nombre = l.Nombre,
                                            CMujer = l.CantidadMujer,
                                            CHombre = l.CantidadHombre,
                                            MMujer = l.MontoMujer,
                                            MHombre = l.MontoHombre
                                        })
                                    }), JsonRequestBehavior.AllowGet);
                case OrigenData.Cementerio:
                    return this.Json(personal_Ano.Personal_Cementerio_Nivel1.OrderBy(r => r.Nombre).Select(r =>
                                    new
                                    {
                                        Item = r.CodTipo,
                                        Lista = r.Personal_Cementerio_Nivel2.OrderBy(l => l.Nombre).Select(l =>
                                        new
                                        {
                                            Nombre = l.Nombre,
                                            CMujer = l.CantidadMujer,
                                            CHombre = l.CantidadHombre,
                                            MMujer = l.MontoMujer,
                                            MHombre = l.MontoHombre
                                        })
                                    }), JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}