using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using AutoMapper;
using Newtonsoft.Json;
using Core.Models.Subsidio;

namespace GastoTransparenteMunicipal.Models
{
    public class SubsidioModel
    {
        public List<Subsidio_N1> Subsidio_Nivel1 { get; set; }
        public List<Subsidio_N3> Subsidio_Nivel3 { get; set; }
        public string JsonSubsidio { get; set; }

        public SubsidioModel()
        {
            this.Subsidio_Nivel1 = new List<Subsidio_N1>();            
        }

        public void Init(GastoTransparenteMunicipalEntities db, int idMunicipality)
        {
            Subsidio_Ano subsidio_Ano = db.Subsidio_Ano.Where(r => r.IdMunicipalidad == idMunicipality).OrderByDescending(r => r.IdAno).First();
            var subsidio_Nivel1 = db.Subsidio_Nivel1.Where(r => r.IdAno == subsidio_Ano.IdAno).ToList();

            foreach (var subsidio_nivel1 in subsidio_Nivel1)
            {
                var subsidio_nivel2 = db.Subsidio_Nivel2.Where(r => r.IdNivel1 == subsidio_nivel1.IdNivel1).ToList();

                var subsidio_n1 = new Subsidio_N1();                
                Mapper.Map(subsidio_nivel1, subsidio_n1);
                Mapper.Map(subsidio_nivel2, subsidio_n1.subsidio_Nivel2);
                this.Subsidio_Nivel1.Add(subsidio_n1);
            }

            LoadJsonNivel1(subsidio_Nivel1);
        }

        public void LoadJsonNivel1(List<Subsidio_Nivel1> subsidio_Nivel1)
        {
            var data = subsidio_Nivel1.OrderBy(r => r.Monto).Select((r, i) => new { name = r.Nombre, size = r.Monto, position = (i + 1) }).ToList();           
            var jsonResult = new {
                name= "document",
                children= data
            };

            this.JsonSubsidio = JsonConvert.SerializeObject(jsonResult);
        }

        public void Load(GastoTransparenteMunicipalEntities db, int year)
        {
            Subsidio_Ano subsidio_Ano = db.Subsidio_Ano.Where(r =>  r.IdAno == year).First();
            var subsidio_Nivel1 = db.Subsidio_Nivel1.Where(r => r.IdAno == subsidio_Ano.IdAno).ToList();

            foreach (var subsidio_nivel1 in subsidio_Nivel1)
            {
                var subsidio_nivel2 = db.Subsidio_Nivel2.Where(r => r.IdNivel1 == subsidio_nivel1.IdNivel1).ToList();

                var subsidio_n1 = new Subsidio_N1();
                Mapper.Map(subsidio_nivel1, subsidio_n1);
                Mapper.Map(subsidio_nivel2, subsidio_n1.subsidio_Nivel2);
                this.Subsidio_Nivel1.Add(subsidio_n1);
            }
            LoadJsonNivel1(subsidio_Nivel1);
        }

        public void Load_Nivel3(GastoTransparenteMunicipalEntities db, int IdNivel2)
        {
            this.Subsidio_Nivel3 = new List<Subsidio_N3>();
            var subsidio_Nivel3 = db.Subsidio_Nivel3.Where(r => r.IdNivel2 == IdNivel2).ToList();
            Mapper.Map(subsidio_Nivel3, this.Subsidio_Nivel3);
        }
    }
}