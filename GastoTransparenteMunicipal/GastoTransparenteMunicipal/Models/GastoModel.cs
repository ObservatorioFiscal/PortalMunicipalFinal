using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Core;
using Newtonsoft.Json;
using Core.Models.Gasto;

namespace GastoTransparenteMunicipal.Models
{
    public class GastoModel
    {
        public List<Gasto_N1> Gasto_Nivel1 { get; set; }
        public List<Gasto_N2> Gasto_Nivel2 { get; set; }
        public List<Gasto_N3> Gasto_Nivel3 { get; set; }
        public List<Gasto_N4> Gasto_Nivel4 { get; set; }

        public string JsonNivel1 { get; set; }
        public string JsonNivel2 { get; set; }
        public string JsonNivel3 { get; set; }
        public string JsonNivel4 { get; set; }

        public GastoModel()
        {
            this.Gasto_Nivel1 = new List<Gasto_N1>();
            this.Gasto_Nivel2 = new List<Gasto_N2>();
            this.Gasto_Nivel3 = new List<Gasto_N3>();
            this.Gasto_Nivel4 = new List<Gasto_N4>();
        }

        public void Init(GastoTransparenteMunicipalEntities db, int idMunicipality,string tipoGasto)
        {
            Gasto_Ano gasto_Ano = db.Gasto_Ano.Where(r => r.IdMunicipalidad == idMunicipality).OrderByDescending(r => r.IdAno).First();
            var gasto_Nivel1 = db.Gasto_Nivel1.Where(r => r.IdAno == gasto_Ano.IdAno && r.Tipo == tipoGasto).ToList();
            Mapper.Map(gasto_Nivel1, this.Gasto_Nivel1);
            LoadJsonNivel1(this.Gasto_Nivel1);
        }

        public void LoadNivel1(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year)
        {
            Gasto_Ano gasto_Ano = db.Gasto_Ano.Where(r => r.IdMunicipalidad == idMunicipality && r.IdAno == year).First();
            var gasto_Nivel1 = db.Gasto_Nivel1.Where(r => r.IdAno == gasto_Ano.IdAno && r.Tipo == tipoGasto).ToList();
            Mapper.Map(gasto_Nivel1, this.Gasto_Nivel1);
            LoadJsonNivel1(this.Gasto_Nivel1);
        }

        public void LoadNivel2(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel1)
        {
            var gasto_Nivel2 = db.Gasto_Nivel2.Where(r => r.Tipo == tipoGasto && r.IdNivel1 == idNivel1).ToList();
            Mapper.Map(gasto_Nivel2, this.Gasto_Nivel2);
            LoadJsonNivel2(this.Gasto_Nivel2);
        }

        public void LoadNivel3(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel2)
        {
            var gasto_Nivel3 = db.Gasto_Nivel3.Where(r => r.Tipo == tipoGasto && r.IdNivel2 == idNivel2).ToList();
            Mapper.Map(gasto_Nivel3, this.Gasto_Nivel3);
            LoadJsonNivel3(this.Gasto_Nivel3);
        }

        public void LoadNivel4(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel3)
        {
            var gasto_Nivel4 = db.Gasto_Nivel4.Where(r => r.Tipo == tipoGasto && r.IdNivel3 == idNivel3).ToList();
            Mapper.Map(gasto_Nivel4, this.Gasto_Nivel4);
            LoadJsonNivel4(this.Gasto_Nivel4);
        }

        public void LoadJsonNivel1(List<Gasto_N1> Gasto_Nivel1)
        {
            var data = Gasto_Nivel1.OrderBy(r => r.MontoGastado).Select(r => new
            {
                name = r.Nombre,
                size = r.MontoGastado,
                tipo = r.Tipo,
                valueTooltip1 = string.Format("{0:N0}", r.MontoGastado.Value),
                valueTooltip2 = string.Format("{0:N0}",r.MontoPresupuestado),
                porcentaje1 = Math.Round((r.PorcentajeGastado.Value) * 100,2),
                porcentaje2 = Math.Round((r.PorcentajePresupuestado.Value) * 100, 2),
                descripcion = r.Descripcion,
                nivel = 1,
                id = r.IdNivel1,
                color = "#193558"
            }).ToList();

            var jsonData = new
            {
                name = "flare",
                children  = data,
                Format = "0"
            };
            this.JsonNivel1 = JsonConvert.SerializeObject(jsonData);
        }

        public void LoadJsonNivel2(List<Gasto_N2> Gasto_Nivel2)
        {
            var data = Gasto_Nivel2.OrderBy(r => r.MontoGastado).Select(r => new
            {
                name = r.Nombre,
                size = r.MontoGastado,
                tipo = r.Tipo,
                valueTooltip1 = string.Format("{0:N0}", r.MontoGastado.Value),
                valueTooltip2 = string.Format("{0:N0}", r.MontoPresupuestado),
                porcentaje1 = Math.Round((r.PorcentajeGastado.Value) * 100, 2),
                porcentaje2 = Math.Round((r.PorcentajePresupuestado.Value) * 100, 2),
                descripcion = r.Descripcion,
                nivel = 1,
                id = r.IdNivel2,
                color = "#193558"
            }).ToList();

            var jsonData = new
            {
                name = "flare",
                children = data,
                Format = "0"
            };
            this.JsonNivel2 = JsonConvert.SerializeObject(jsonData);
        }

        public void LoadJsonNivel3(List<Gasto_N3> Gasto_Nivel3)
        {
            var data = Gasto_Nivel3.OrderBy(r => r.MontoGastado).Select(r => new
            {
                name = r.Nombre,
                size = r.MontoGastado,
                tipo = r.Tipo,
                valueTooltip1 = string.Format("{0:N0}", r.MontoGastado.Value),
                valueTooltip2 = string.Format("{0:C0}", r.MontoPresupuestado),
                porcentaje1 = Math.Round((r.PorcentajeGastado.Value) * 100, 2),
                porcentaje2 = Math.Round((r.PorcentajePresupuestado.Value) * 100, 2),
                descripcion = r.Descripcion,
                nivel = 1,
                id = r.IdNivel3,
                color = "#193558"
            }).ToList();

            var jsonData = new
            {
                name = "flare",
                children = data,
                Format = "0"
            };
            this.JsonNivel3 = JsonConvert.SerializeObject(jsonData);
        }

        public void LoadJsonNivel4(List<Gasto_N4> Gasto_Nivel4)
        {
            var data = Gasto_Nivel4.OrderBy(r => r.MontoGastado).Select(r => new
            {
                name = r.Nombre,
                size = r.MontoGastado,
                tipo = r.Tipo,
                valueTooltip1 = string.Format("{0:N0}", r.MontoGastado.Value),
                valueTooltip2 = string.Format("{0:N0}", r.MontoPresupuestado),
                porcentaje1 = Math.Round((r.PorcentajeGastado.Value) * 100, 2),
                porcentaje2 = Math.Round((r.PorcentajePresupuestado.Value) * 100, 2),
                descripcion = r.Descripcion,
                nivel = 1,
                id = r.IdNivel4,
                color = "#193558"
            }).ToList();

            var jsonData = new
            {
                name = "flare",
                children = data,
                Format = "0"
            };
            this.JsonNivel4 = JsonConvert.SerializeObject(jsonData);
        }
    }
}