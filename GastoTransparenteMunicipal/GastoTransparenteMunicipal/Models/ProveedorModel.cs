using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using Newtonsoft.Json;
using AutoMapper;
using Core.Models.Proveedor;

namespace GastoTransparenteMunicipal.Models
{
    public class ProveedorModel
    {
        public string JsonProveedor { get; set; }
        public string JsonTabla { get; set; }
        public string JsonDetalle { get; set; }
        public List<Proveedor_Nivel1> Proveedor_Nivel1 { get; set; }
        public List<Proveedor_Nivel2> Proveedor_Nivel2 { get; set; }
        int Ordennumero = 0;
        public ProveedorModel()
        {
            this.JsonProveedor = string.Empty;
            this.Proveedor_Nivel1 = new List<Proveedor_Nivel1>();
            this.Proveedor_Nivel2 = new List<Proveedor_Nivel2>();
        }
        
        public void WordCloud(GastoTransparenteMunicipalEntities db, int takeElements, int year, int origenData)
        {
            LoadNivel1(db, year, origenData, takeElements);
            LoadJsonNivel1();
        }

        public void ListaCompleta(GastoTransparenteMunicipalEntities db, int year, int origenData, int orden)
        {
            Ordennumero = orden;
            LoadNivel1Tabla(db, year, origenData);
            LoadJsonNivel1Tabla();
        }

        public void ListaDetalle(GastoTransparenteMunicipalEntities db, int Idnivel1, int origenData)
        {
            LoadNivel2(db, Idnivel1, origenData);
            LoadJsonNivel2();
        }


        private List<Proveedor_Nivel1> LoadNivel1(GastoTransparenteMunicipalEntities db, int year, int origenData, int takeElements)
        {
            object nivel1;
            switch (origenData)
            {
                case OrigenData.Adm:
                    nivel1 = db.Proveedor_Adm_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();
                    Mapper.Map((List<Proveedor_Adm_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Educacion:
                    nivel1 = db.Proveedor_Educacion_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();
                    Mapper.Map((List<Proveedor_Educacion_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Salud:
                    nivel1 = db.Proveedor_Salud_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();
                    Mapper.Map((List<Proveedor_Salud_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Cementerio:
                    nivel1 = db.Proveedor_Cementerio_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();
                    Mapper.Map((List<Proveedor_Cementerio_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.MunicipioTotal:
                    nivel1 = db.Proveedor_Total_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();

                    Mapper.Map((List<Proveedor_Total_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                default:
                    nivel1 = db.Proveedor_Total_Nivel1.Where(r => r.IdAno == year).OrderByDescending(r => r.Monto).Take(takeElements).ToList();
                    Mapper.Map((List<Proveedor_Total_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;
            }
        }
        private List<Proveedor_Nivel1> LoadNivel1Tabla(GastoTransparenteMunicipalEntities db, int year, int origenData)
        {
            object nivel1;
            switch (origenData)
            {
                case OrigenData.Adm:
                    nivel1 = db.Proveedor_Adm_Nivel1.Where(r => r.IdAno == year).ToList();
                    Mapper.Map((List<Proveedor_Adm_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Educacion:
                    nivel1 = db.Proveedor_Educacion_Nivel1.Where(r => r.IdAno == year).ToList();
                    Mapper.Map((List<Proveedor_Educacion_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Salud:
                    nivel1 = db.Proveedor_Salud_Nivel1.Where(r => r.IdAno == year).ToList();
                    Mapper.Map((List<Proveedor_Salud_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.Cementerio:
                    nivel1 = db.Proveedor_Cementerio_Nivel1.Where(r => r.IdAno == year).ToList();
                    Mapper.Map((List<Proveedor_Cementerio_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                case OrigenData.MunicipioTotal:
                    nivel1 = db.Proveedor_Total_Nivel1.Where(r => r.IdAno == year).ToList();

                    Mapper.Map((List<Proveedor_Total_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;

                default:
                    nivel1 = db.Proveedor_Total_Nivel1.Where(r => r.IdAno == year).ToList();
                    Mapper.Map((List<Proveedor_Total_Nivel1>)nivel1, this.Proveedor_Nivel1);
                    return this.Proveedor_Nivel1;
            }
        }

        private List<Proveedor_Nivel2> LoadNivel2(GastoTransparenteMunicipalEntities db, int Idnivel1, int origenData)
        {
            object nivel2;
            switch (origenData)
            {
                case OrigenData.Adm:
                    nivel2 = db.Proveedor_Adm_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();
                    Mapper.Map((List<Proveedor_Adm_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;

                case OrigenData.Educacion:
                    nivel2 = db.Proveedor_Educacion_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();
                    Mapper.Map((List<Proveedor_Educacion_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;

                case OrigenData.Salud:
                    nivel2 = db.Proveedor_Salud_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();
                    Mapper.Map((List<Proveedor_Salud_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;

                case OrigenData.Cementerio:
                    nivel2 = db.Proveedor_Cementerio_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();
                    Mapper.Map((List<Proveedor_Cementerio_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;

                case OrigenData.MunicipioTotal:
                    nivel2 = db.Proveedor_Total_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();

                    Mapper.Map((List<Proveedor_Total_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;
                default:
                    nivel2 = db.Proveedor_Total_Nivel2.Where(r => r.IdNIvel1 == Idnivel1).ToList();
                    Mapper.Map((List<Proveedor_Total_Nivel2>)nivel2, this.Proveedor_Nivel2);
                    return this.Proveedor_Nivel2;
            }
        }


        private void LoadJsonNivel1()
        {
            var jsonProveedor = this.Proveedor_Nivel1.Select(r => r.Nombre);
            var jsonMonto = this.Proveedor_Nivel1.Select(r => r.Monto);

            var proveedor = new
            {
                proveedor = jsonProveedor,
                monto = jsonMonto
            };

            this.JsonProveedor = JsonConvert.SerializeObject(proveedor);
        }

        private void LoadJsonNivel1Tabla()
        {
            switch (Ordennumero)
            {
                case 1://Nombre ascendente
                    this.Proveedor_Nivel1 = this.Proveedor_Nivel1.OrderByDescending(r => r.Nombre).ToList();
                    break;
                case 2://Nombre decendente
                    this.Proveedor_Nivel1 = this.Proveedor_Nivel1.OrderBy(r => r.Nombre).ToList();
                    break;
                case 3://Monto ascendente
                    this.Proveedor_Nivel1 = this.Proveedor_Nivel1.OrderByDescending(r => r.Monto).ToList();
                    break;
                default://Monto decendente o 4
                    this.Proveedor_Nivel1 = this.Proveedor_Nivel1.OrderBy(r => r.Monto).ToList();
                    break;
            }
            var proveedor = this.Proveedor_Nivel1.Select(r => new
            {
                r.IdNivel1,
                r.Nombre,
                r.Monto
            });

            this.JsonTabla = JsonConvert.SerializeObject(proveedor);
        }

        private void LoadJsonNivel2()
        {
            var proveedor = this.Proveedor_Nivel2.Select(r => new
            {
                r.Nombre,
                r.Monto,
                r.Glosa,
                r.Area
            });

            this.JsonDetalle = JsonConvert.SerializeObject(proveedor);
        }
    }
}