using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using AutoMapper;

namespace GastoTransparenteMunicipal.Models
{
    public class PersonalModel
    {
        public void LoadPersonal(GastoTransparenteMunicipalEntities db, int idMunicipality, int year, int origenData)
        {
            Personal_Ano personal_Ano = db.Personal_Ano.FirstOrDefault(r => r.IdMunicipalidad == idMunicipality && r.Ano == year);

            switch (origenData)
            {
                case OrigenData.Adm:
                    var personales_adm_nivel1 = personal_Ano.Personal_Adm_Nivel1.ToList();
                    break;
                case OrigenData.Salud:
                    var personales_salud_nivel1 = personal_Ano.Personal_Salud_Nivel1.ToList();                 
                    break;
                case OrigenData.Educacion:
                    var personales_educacion_nivel1 = personal_Ano.Personal_Educacion_Nivel1.ToList();
                    break;
                case OrigenData.Cementerio:
                    var personales_cementerio_nivel1 = personal_Ano.Personal_Educacion_Nivel1.ToList();                  
                    break;
            } 
        }

        #region SOBRA
        //private void LoadPersonalNivel2(GastoTransparenteMunicipalEntities db, Personal_Ano personal_Ano, Personal_Total_Nivel1 personal_nivel1)
        //{
        //    object personal_nivel2 = null;
        //    switch (personal_nivel1.CodTipo)
        //    {
        //        case OrigenData.Antiguedad:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Antiguedad.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_Antiguedad.Personal_Nivel2);
        //            break;

        //        case OrigenData.Calidadjuridica:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_CalidadJuridica.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_CalidadJuridica.Personal_Nivel2);
        //            break;

        //        case OrigenData.Estamento:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Estamento.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_Estamento.Personal_Nivel2);
        //            break;

        //        case OrigenData.Grado:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Grado.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_Grado.Personal_Nivel2);
        //            break;

        //        case OrigenData.Niveleducacion:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_NivelEducacion.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_NivelEducacion.Personal_Nivel2);
        //            break;

        //        case OrigenData.Profesiones:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Profesiones.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_Profesiones.Personal_Nivel2);
        //            break;

        //        case OrigenData.Rangoetario:
        //            personal_nivel2 = db.Personal_Total_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_RangoEtario.Personal_Nivel1);
        //            Mapper.Map((Personal_Total_Nivel2)personal_nivel2, this.Personal_RangoEtario.Personal_Nivel2);
        //            break;

        //        default: break;

        //    }
        //}
        //private void LoadPersonalNivel2(GastoTransparenteMunicipalEntities db, Personal_Ano personal_Ano, Personal_Educacion_Nivel1 personal_nivel1)
        //{
        //    object personal_nivel2 = null;
        //    switch (personal_nivel1.CodTipo)
        //    {
        //        case OrigenData.Antiguedad:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Antiguedad.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_Antiguedad.Personal_Nivel2);
        //            break;

        //        case OrigenData.Calidadjuridica:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_CalidadJuridica.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_CalidadJuridica.Personal_Nivel2);
        //            break;

        //        case OrigenData.Estamento:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Estamento.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_Estamento.Personal_Nivel2);
        //            break;

        //        case OrigenData.Grado:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Grado.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_Grado.Personal_Nivel2);
        //            break;

        //        case OrigenData.Niveleducacion:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_NivelEducacion.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_NivelEducacion.Personal_Nivel2);
        //            break;

        //        case OrigenData.Profesiones:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Profesiones.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_Profesiones.Personal_Nivel2);
        //            break;

        //        case OrigenData.Rangoetario:
        //            personal_nivel2 = db.Personal_Educacion_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_RangoEtario.Personal_Nivel1);
        //            Mapper.Map((Personal_Educacion_Nivel2)personal_nivel2, this.Personal_RangoEtario.Personal_Nivel2);
        //            break;

        //        default: break;

        //    }
        //}
        //private void LoadPersonalNivel2(GastoTransparenteMunicipalEntities db, Personal_Ano personal_Ano, Personal_Salud_Nivel1 personal_nivel1)
        //{
        //    object personal_nivel2 = null;
        //    switch (personal_nivel1.CodTipo)
        //    {
        //        case OrigenData.Antiguedad:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Antiguedad.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_Antiguedad.Personal_Nivel2);
        //            break;

        //        case OrigenData.Calidadjuridica:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_CalidadJuridica.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_CalidadJuridica.Personal_Nivel2);
        //            break;

        //        case OrigenData.Estamento:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Estamento.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_Estamento.Personal_Nivel2);
        //            break;

        //        case OrigenData.Grado:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Grado.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_Grado.Personal_Nivel2);
        //            break;

        //        case OrigenData.Niveleducacion:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_NivelEducacion.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_NivelEducacion.Personal_Nivel2);
        //            break;

        //        case OrigenData.Profesiones:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Profesiones.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_Profesiones.Personal_Nivel2);
        //            break;

        //        case OrigenData.Rangoetario:
        //            personal_nivel2 = db.Personal_Salud_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_RangoEtario.Personal_Nivel1);
        //            Mapper.Map((Personal_Salud_Nivel2)personal_nivel2, this.Personal_RangoEtario.Personal_Nivel2);
        //            break;

        //        default: break;

        //    }
        //}
        //private void LoadPersonalNivel2(GastoTransparenteMunicipalEntities db, Personal_Ano personal_Ano, Personal_Adm_Nivel1 personal_nivel1)
        //{
        //    object personal_nivel2 = null;
        //    switch (personal_nivel1.CodTipo)
        //    {
        //        case OrigenData.Antiguedad:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Antiguedad.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_Antiguedad.Personal_Nivel2);
        //            break;

        //        case OrigenData.Calidadjuridica:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_CalidadJuridica.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_CalidadJuridica.Personal_Nivel2);
        //            break;

        //        case OrigenData.Estamento:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Estamento.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_Estamento.Personal_Nivel2);
        //            break;

        //        case OrigenData.Grado:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Grado.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_Grado.Personal_Nivel2);
        //            break;

        //        case OrigenData.Niveleducacion:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_NivelEducacion.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_NivelEducacion.Personal_Nivel2);
        //            break;

        //        case OrigenData.Profesiones:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Profesiones.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_Profesiones.Personal_Nivel2);
        //            break;

        //        case OrigenData.Rangoetario:
        //            personal_nivel2 = db.Personal_Adm_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_RangoEtario.Personal_Nivel1);
        //            Mapper.Map((Personal_Adm_Nivel2)personal_nivel2, this.Personal_RangoEtario.Personal_Nivel2);
        //            break;

        //        default: break;

        //    }
        //}
        //private void LoadPersonalNivel2(GastoTransparenteMunicipalEntities db, Personal_Ano personal_Ano, Personal_Cementerio_Nivel1 personal_nivel1)
        //{
        //    object personal_nivel2 = null;
        //    switch (personal_nivel1.CodTipo)
        //    {
        //        case OrigenData.Antiguedad:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Antiguedad.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_Antiguedad.Personal_Nivel2);
        //            break;

        //        case OrigenData.Calidadjuridica:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_CalidadJuridica.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_CalidadJuridica.Personal_Nivel2);
        //            break;

        //        case OrigenData.Estamento:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Estamento.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_Estamento.Personal_Nivel2);
        //            break;

        //        case OrigenData.Grado:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Grado.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_Grado.Personal_Nivel2);
        //            break;

        //        case OrigenData.Niveleducacion:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_NivelEducacion.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_NivelEducacion.Personal_Nivel2);
        //            break;

        //        case OrigenData.Profesiones:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_Profesiones.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_Profesiones.Personal_Nivel2);
        //            break;

        //        case OrigenData.Rangoetario:
        //            personal_nivel2 = db.Personal_Cementerio_Nivel2.Where(r => r.IdNivel1 == personal_nivel1.IdNivel1).ToList();
        //            Mapper.Map(personal_nivel1, this.Personal_RangoEtario.Personal_Nivel1);
        //            Mapper.Map((Personal_Cementerio_Nivel2)personal_nivel2, this.Personal_RangoEtario.Personal_Nivel2);
        //            break;

        //        default: break;

        //    }
        //}

        #endregion
    }
}