using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Core.Models.Ingreso;
using Core;

namespace GastoTransparenteMunicipal.Models
{
    public class IngresoModel
    {
        public List<Ingreso_N1> Ingreso_Nivel1 { get; set; }
        public List<Ingreso_N2> Ingreso_Nivel2 { get; set; }
        public List<Ingreso_N3> Ingreso_Nivel3 { get; set; }
        public List<Ingreso_N4> Ingreso_Nivel4 { get; set; }

        public IngresoModel()
        {
            this.Ingreso_Nivel1 = new List<Ingreso_N1>();
            this.Ingreso_Nivel2 = new List<Ingreso_N2>();
            this.Ingreso_Nivel3 = new List<Ingreso_N3>();
            this.Ingreso_Nivel4 = new List<Ingreso_N4>();
        }

        public void Init(GastoTransparenteMunicipalEntities db, int idMunicipality,string tipoGasto)
        {
            Ingreso_Ano ingreso_Ano = db.Ingreso_Ano.Where(r => r.IdMunicipalidad == idMunicipality).OrderByDescending(r => r.IdAno).First();
            var ingreso_Nivel1 = db.Ingreso_Nivel1.Where(r => r.IdAno == ingreso_Ano.IdAno && r.Tipo == tipoGasto).ToList();
            Mapper.Map(ingreso_Nivel1, this.Ingreso_Nivel1);
        }

        public void LoadNivel1(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year)
        {
            Ingreso_Ano ingreso_Ano = db.Ingreso_Ano.Where(r => r.IdMunicipalidad == idMunicipality && r.IdAno == year).First();
            var ingreso_Nivel1 = db.Ingreso_Nivel1.Where(r => r.IdAno == ingreso_Ano.IdAno && r.Tipo == tipoGasto).ToList();
            Mapper.Map(ingreso_Nivel1, this.Ingreso_Nivel1);
        }

        public void LoadNivel2(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel1)
        {
            var ingreso_Nivel2 = db.Ingreso_Nivel2.Where(r => r.Tipo == tipoGasto && r.IdNivel1 == idNivel1).ToList();
            Mapper.Map(ingreso_Nivel2, this.Ingreso_Nivel2);
        }

        public void LoadNivel3(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel2)
        {
            var ingreso_Nivel3 = db.Ingreso_Nivel3.Where(r => r.Tipo == tipoGasto && r.IdNivel2 == idNivel2).ToList();
            Mapper.Map(ingreso_Nivel3, this.Ingreso_Nivel3);
        }

        public void LoadNivel4(GastoTransparenteMunicipalEntities db, int idMunicipality, string tipoGasto, int year, int idNivel3)
        {
            var ingreso_Nivel4 = db.Ingreso_Nivel4.Where(r => r.Tipo == tipoGasto && r.IdNivel3 == idNivel3).ToList();
            Mapper.Map(ingreso_Nivel4, this.Ingreso_Nivel4);
        }
    }
}