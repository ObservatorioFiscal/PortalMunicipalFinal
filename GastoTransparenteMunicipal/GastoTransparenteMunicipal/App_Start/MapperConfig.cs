using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using Core.Models.Corporacion;
using Core.Models.Gasto;
using Core.Models.Ingreso;
using Core.Models.Personal;
using Core.Models.Proveedor;
using Core.Models.Subsidio;
using NPOI.SS.UserModel;

namespace GastoTransparenteMunicipal
{
    public class MapperConfig
    {
        public static void Mapping()
        {
            Mapper.Initialize(cfg => {

                #region Carga de archivo
                
                cfg.CreateMap<IRow, Core.IngresoInformev2>()
                 .ForMember(dst => dst.Codigo, src => src.MapFrom(e => e.GetCell(0).StringCellValue))
                 .ForMember(dst => dst.MontoPresupuestado, src => src.MapFrom(e => Math.Round(e.GetCell(1).NumericCellValue / 1000000.0, 0)))
                 .ForMember(dst => dst.MontoGastado, src => src.MapFrom(e => Math.Round(e.GetCell(2).NumericCellValue / 1000000.0, 0)));

                cfg.CreateMap<IRow, Core.GastoInformev2>()
                .ForMember(dst => dst.Codigo, src => src.MapFrom(e => e.GetCell(0).StringCellValue))
                .ForMember(dst => dst.Cuenta, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.MontoPresupuestado, src => src.MapFrom(e => Math.Round(e.GetCell(2).NumericCellValue / 1000000.0, 0)))
                .ForMember(dst => dst.MontoGastado, src => src.MapFrom(e => Math.Round(e.GetCell(3).NumericCellValue / 1000000.0, 0)));
                
                cfg.CreateMap<IRow, Core.SubsidioInforme>()
                .ForMember(dst => dst.FechaDecreto, src => src.MapFrom(e => e.GetCell(0).StringCellValue))
                .ForMember(dst => dst.Categoria, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Organizacion, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.MontoAporte, src => src.MapFrom(e => e.GetCell(3).NumericCellValue))
                .ForMember(dst => dst.ObjetivoDelAporte, src => src.MapFrom(e => e.GetCell(4).StringCellValue));

                cfg.CreateMap<IRow, Core.Personal_AdmInforme>()
                .ForMember(dst => dst.Genero, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Edad, src => src.MapFrom(e => e.GetCell(2).NumericCellValue))
                .ForMember(dst => dst.CalidadJuridica, src => src.MapFrom(e => e.GetCell(3).StringCellValue))
                .ForMember(dst => dst.Profesion, src => src.MapFrom(e => e.GetCell(4).StringCellValue))
                .ForMember(dst => dst.NivelAcademico, src => src.MapFrom(e => e.GetCell(5).StringCellValue))
                .ForMember(dst => dst.Estamento, src => src.MapFrom(e => e.GetCell(6).StringCellValue))
                .ForMember(dst => dst.Grado, src => src.MapFrom(e => e.GetCell(7).NumericCellValue))
                .ForMember(dst => dst.Antiguedad, src => src.MapFrom(e => e.GetCell(8).CellType == CellType.String ? e.GetCell(7).StringCellValue : e.GetCell(7).NumericCellValue.ToString()))
                //.ForMember(dst => dst.AREA, src => src.MapFrom(e => e.GetCell(8).StringCellValue))
                .ForMember(dst => dst.SueldoHaberes, src => src.MapFrom(e => e.GetCell(9).NumericCellValue));

                cfg.CreateMap<IRow, Core.Personal_SaludInforme>()
                .ForMember(dst => dst.Genero, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Edad, src => src.MapFrom(e => e.GetCell(2).NumericCellValue))
                .ForMember(dst => dst.CalidadJuridica, src => src.MapFrom(e => e.GetCell(3).StringCellValue))
                .ForMember(dst => dst.Profesion, src => src.MapFrom(e => e.GetCell(4).StringCellValue))
                .ForMember(dst => dst.NivelAcademico, src => src.MapFrom(e => e.GetCell(5).StringCellValue))
                .ForMember(dst => dst.Estamento, src => src.MapFrom(e => e.GetCell(6).StringCellValue))
                .ForMember(dst => dst.Grado, src => src.MapFrom(e => e.GetCell(7).NumericCellValue))
                .ForMember(dst => dst.Antiguedad, src => src.MapFrom(e => e.GetCell(8).CellType == CellType.String ? e.GetCell(7).StringCellValue : e.GetCell(7).NumericCellValue.ToString()))
                //.ForMember(dst => dst.AREA, src => src.MapFrom(e => e.GetCell(8).StringCellValue))
                .ForMember(dst => dst.SueldoHaberes, src => src.MapFrom(e => e.GetCell(9).NumericCellValue));

                cfg.CreateMap<IRow, Core.Personal_EducacionInforme>()
                .ForMember(dst => dst.Genero, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Edad, src => src.MapFrom(e => e.GetCell(2).NumericCellValue))
                .ForMember(dst => dst.CalidadJuridica, src => src.MapFrom(e => e.GetCell(3).StringCellValue))
                .ForMember(dst => dst.Profesion, src => src.MapFrom(e => e.GetCell(4).StringCellValue))
                .ForMember(dst => dst.NivelAcademico, src => src.MapFrom(e => e.GetCell(5).StringCellValue))
                .ForMember(dst => dst.Estamento, src => src.MapFrom(e => e.GetCell(6).StringCellValue))
                .ForMember(dst => dst.Grado, src => src.MapFrom(e => e.GetCell(7).NumericCellValue))
                .ForMember(dst => dst.Antiguedad, src => src.MapFrom(e => e.GetCell(8).CellType == CellType.String ? e.GetCell(7).StringCellValue : e.GetCell(7).NumericCellValue.ToString()))
                .ForMember(dst => dst.SueldoHaberes, src => src.MapFrom(e => e.GetCell(9).NumericCellValue));

                cfg.CreateMap<IRow, Core.Personal_CementerioInforme>()
                .ForMember(dst => dst.Genero, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Edad, src => src.MapFrom(e => e.GetCell(2).NumericCellValue))
                .ForMember(dst => dst.CalidadJuridica, src => src.MapFrom(e => e.GetCell(3).StringCellValue))
                .ForMember(dst => dst.Profesion, src => src.MapFrom(e => e.GetCell(4).StringCellValue))
                .ForMember(dst => dst.NivelAcademico, src => src.MapFrom(e => e.GetCell(5).StringCellValue))
                .ForMember(dst => dst.Estamento, src => src.MapFrom(e => e.GetCell(6).StringCellValue))
                .ForMember(dst => dst.Grado, src => src.MapFrom(e => e.GetCell(7).NumericCellValue))
                .ForMember(dst => dst.Antiguedad, src => src.MapFrom(e => e.GetCell(8).CellType == CellType.String ? e.GetCell(7).StringCellValue : e.GetCell(7).NumericCellValue.ToString()))
                //.ForMember(dst => dst.AREA, src => src.MapFrom(e => e.GetCell(8).StringCellValue))
                .ForMember(dst => dst.SueldoHaberes, src => src.MapFrom(e => e.GetCell(9).NumericCellValue));

                cfg.CreateMap<IRow, Core.Proveedor_AdmInforme>()
                .ForMember(dst => dst.NumeroOrdenCompra, src => src.MapFrom(e => e.GetCell(0).NumericCellValue))
                .ForMember(dst => dst.Glosa, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Proveedor, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.Monto, src => src.MapFrom(e => e.GetCell(3).NumericCellValue));

                cfg.CreateMap<IRow, Core.Proveedor_SaludInforme>()
                .ForMember(dst => dst.NumeroOrdenCompra, src => src.MapFrom(e => e.GetCell(0).NumericCellValue))
                .ForMember(dst => dst.Glosa, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Proveedor, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.Monto, src => src.MapFrom(e => e.GetCell(3).NumericCellValue));

                cfg.CreateMap<IRow, Core.Proveedor_EducacionInforme>()
                .ForMember(dst => dst.NumeroOrdenCompra, src => src.MapFrom(e => e.GetCell(0).NumericCellValue))
                .ForMember(dst => dst.Glosa, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Proveedor, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.Monto, src => src.MapFrom(e => e.GetCell(3).NumericCellValue));

                cfg.CreateMap<IRow, Core.Proveedor_CementerioInforme>()
                .ForMember(dst => dst.NumeroOrdenCompra, src => src.MapFrom(e => e.GetCell(0).NumericCellValue))
                .ForMember(dst => dst.Glosa, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Proveedor, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.Monto, src => src.MapFrom(e => e.GetCell(3).NumericCellValue));

                cfg.CreateMap<IRow, Core.CorporacionInforme>()
                .ForMember(dst => dst.ObjetivoDelAporte, src => src.MapFrom(e => e.GetCell(0).StringCellValue))
                .ForMember(dst => dst.Origen, src => src.MapFrom(e => e.GetCell(1).StringCellValue))
                .ForMember(dst => dst.Destino, src => src.MapFrom(e => e.GetCell(2).StringCellValue))
                .ForMember(dst => dst.MontoAportado, src => src.MapFrom(e => e.GetCell(3).NumericCellValue));
                #endregion

                #region proveedores

                cfg.CreateMap<Core.Proveedor_Adm_Nivel1, Proveedor_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Total_Nivel1, Proveedor_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Salud_Nivel1, Proveedor_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Educacion_Nivel1, Proveedor_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Cementerio_Nivel1, Proveedor_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Adm_Nivel2, Proveedor_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Total_Nivel2, Proveedor_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Salud_Nivel2, Proveedor_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Educacion_Nivel2, Proveedor_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Proveedor_Cementerio_Nivel2, Proveedor_Nivel2>()
                .ReverseMap();
                #endregion

                #region personal
                cfg.CreateMap<Core.Personal_Adm_Nivel1, Personal_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Salud_Nivel1, Personal_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Educacion_Nivel1, Personal_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Cementerio_Nivel1, Personal_Nivel1>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Adm_Nivel2, Personal_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Salud_Nivel2, Personal_Nivel2>()
                .ReverseMap();
                
                cfg.CreateMap<Core.Personal_Educacion_Nivel2, Personal_Nivel2>()
                .ReverseMap();

                cfg.CreateMap<Core.Personal_Cementerio_Nivel2, Personal_Nivel2>()
                .ReverseMap();

                #endregion

                #region Subsidio
                cfg.CreateMap<Core.Subsidio_Nivel1, Subsidio_N1>()
                .ForMember(dst => dst.subsidio_Nivel2, src => src.Ignore())
                .ReverseMap();

                cfg.CreateMap<Core.Subsidio_Nivel2, Subsidio_N2>()
                .ForMember(dst => dst.subsidio_Nivel3, src => src.Ignore())
                .ReverseMap();

                cfg.CreateMap<Core.Subsidio_Nivel3, Subsidio_N3>()
                .ReverseMap();
                #endregion

                #region corporacion
                cfg.CreateMap<Core.Corporacion_Nivel1, Corporacion_N1>()
                .ReverseMap();
                #endregion

                #region Gasto
                cfg.CreateMap<Core.Gasto_Nivel1, Gasto_N1>()
                .ReverseMap();

                cfg.CreateMap<Core.Gasto_Nivel2, Gasto_N2>()
                .ReverseMap();

                cfg.CreateMap<Core.Gasto_Nivel3, Gasto_N3>()
                .ReverseMap();

                cfg.CreateMap<Core.Gasto_Nivel4, Gasto_N4>()
                .ReverseMap();
                #endregion

                #region Ingreso
                cfg.CreateMap<Core.Ingreso_Nivel1, Ingreso_N1>()
                .ReverseMap();

                cfg.CreateMap<Core.Ingreso_Nivel2, Ingreso_N2>()
                .ReverseMap();

                cfg.CreateMap<Core.Ingreso_Nivel3, Ingreso_N3>()
                .ReverseMap();

                cfg.CreateMap<Core.Ingreso_Nivel4, Ingreso_N4>()
                .ReverseMap();
                #endregion

                #region

                cfg.CreateMap<Core.IngresoInformev2, Core.Models.Ingreso.IngresoInforme>();

                cfg.CreateMap<Core.GastoInformev2, Core.Models.Gasto.GastoInforme>();

                cfg.CreateMap<Core.CorporacionInforme, Core.Models.Corporacion.CorporacionInforme>();

                cfg.CreateMap<Core.CorporacionInforme, Core.Models.Corporacion.CorporacionInforme>();

                cfg.CreateMap<Core.SubsidioInforme, Core.Models.Subsidio.SubsidioInforme>();


                cfg.CreateMap<Core.Personal_AdmInforme, Core.Models.Personal.PersonalInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Administracion Municipal"));

                cfg.CreateMap<Core.Personal_CementerioInforme, Core.Models.Personal.PersonalInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Cementerio"));

                cfg.CreateMap<Core.Personal_EducacionInforme, Core.Models.Personal.PersonalInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Educacion"));

                cfg.CreateMap<Core.Personal_SaludInforme, Core.Models.Personal.PersonalInforme>()               
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Salud"));


                cfg.CreateMap<Core.Proveedor_AdmInforme, Core.Models.Proveedor.ProveedorInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Administracion Municipal"));

                cfg.CreateMap<Core.Proveedor_CementerioInforme, Core.Models.Proveedor.ProveedorInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Cementerio"));

                cfg.CreateMap<Core.Proveedor_EducacionInforme, Core.Models.Proveedor.ProveedorInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Educacion"));

                cfg.CreateMap<Core.Proveedor_SaludInforme, Core.Models.Proveedor.ProveedorInforme>()
                .ForMember(dst => dst.Categoria, src => src.UseValue<string>("Salud"));
                #endregion
            });
        }
    }
}
