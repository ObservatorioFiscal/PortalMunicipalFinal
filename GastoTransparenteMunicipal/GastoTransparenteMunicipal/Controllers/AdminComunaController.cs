using System;
using System.Collections.Generic;
using GastoTransparenteMunicipal.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using System.IO;
using GastoTransparenteMunicipal.Helpers;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using System.Data.Entity;
using System.Data;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using AutoMapper;

namespace GastoTransparenteMunicipal.Controllers
{
    [AuthorizeComuna]
    public class AdminComunaController : BaseController
    {
        private const string errorPassword = "Error Password";
        private const string errorMessage = "Error en columna numero ";
        private const string okMessage = "OK";

        public StringBuilder StringCSV<T>(List<T> listItem)
        {
            ConvertTo convert = new ConvertTo();            
            var dt = convert.DataTable(listItem);

            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);

            sb.AppendLine(string.Join("|", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join("|", fields));
            }            

            return sb;
        }

        public string UploadCSV(string fileName, StringBuilder data)
        {
            BlobService blobService = new BlobService(GetAccountName, GetAccountKey);
            var blob = blobService.UploadBlob(fileName, "dataset", data);
            return blob.Uri.AbsoluteUri;
        }
        
        public ActionResult Index()
        {
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.administracion = true;
            ViewBag.admimuni = true;
            ViewBag.anoscargados = municipalidad.Gasto_Ano.ToList() ;
            ViewBag.activos = new bool[4]{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            return View();
        }

        public ActionResult CargaDatos()
        {
            var municipalidad = GetCurrentIdMunicipality();            
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.administracion = true;
            ViewBag.admimuni = true;
            List<Anos_Invisible> lista = db.Anos_Invisible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).ToList();
            ViewBag.contador = lista.Count;
            ViewBag.Anos = new SelectList(lista, "IdAno", "Nombre");
            ViewBag.activos = new bool[4]{
                municipalidad.Act_Proveedor,municipalidad.Act_Subsidio,municipalidad.Act_Corporacion,municipalidad.Act_Personal
            };
            return View();
        }

        [HttpPost]
        public ActionResult CargaDatos(int id)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.SaveChanges();

            Gasto_Ano gasto = db.Gasto_Ano.Find(id);

            var gastos = db.Gasto_Ano.Where(            r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();
            var ingresos = db.Ingreso_Ano.Where(        r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();
            var proveedors = db.Proveedor_Ano.Where(    r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();
            var subsidios = db.Subsidio_Ano.Where(      r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();
            var corporacions = db.Corporacion_Ano.Where(r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();
            var personals = db.Personal_Ano.Where(      r => r.Ano == gasto.Ano &&  r.IdMunicipalidad == gasto.IdMunicipalidad).ToList();

            if (gastos.Count > 0)
            {
                for (int i = 0; i < gastos.Count; i++)
                {
                    if(gastos[i].Semestre== gasto.Semestre)
                    {
                        gastos[i].Activo = true;
                    }
                    else
                    {
                        gastos[i].Activo = false;
                        db.SP_DeleteGasto(gastos[i].IdAno);
                    }
                }
            }
            if (ingresos.Count > 0)
            {
                for (int i = 0; i < ingresos.Count; i++)
                {
                    if (ingresos[i].Semestre == gasto.Semestre)
                    {
                        ingresos[i].Activo = true;
                    }
                    else
                    {
                        ingresos[i].Activo = false;
                        db.SP_DeleteIngreso(ingresos[i].IdAno);
                    }
                    
                }
            }
            if (proveedors.Count > 0)
            {
                for (int i = 0; i < proveedors.Count; i++)
                {
                    if (proveedors[i].Semestre == gasto.Semestre)
                    {
                        proveedors[i].Activo = true;
                    }
                    else
                    {
                        proveedors[i].Activo = false;
                        db.SP_DeleteProveedor(proveedors[i].IdAno);
                    }

                }
            }
            if (subsidios.Count > 0)
            {
                for (int i = 0; i < subsidios.Count; i++)
                {
                    if (subsidios[i].Semestre == gasto.Semestre)
                    {
                        subsidios[i].Activo = true;
                    }
                    else
                    {
                        subsidios[i].Activo = false;
                        db.SP_DeleteSubsidio(subsidios[i].IdAno);
                    }

                }
            }
            if (corporacions.Count > 0)
            {
                for (int i = 0; i < corporacions.Count; i++)
                {
                    if (corporacions[i].Semestre == gasto.Semestre)
                    {
                        corporacions[i].Activo = true;
                    }
                    else
                    {
                        corporacions[i].Activo = false;
                        db.SP_DeleteCorporacion(corporacions[i].IdAno);
                    }

                }
            }
            if (personals.Count > 0)
            {
                for (int i = 0; i < personals.Count; i++)
                {
                    if (personals[i].Semestre == gasto.Semestre)
                    {
                        personals[i].Activo = true;
                    }
                    else
                    {
                        personals[i].Activo = false;
                        db.SP_DeletePersonal(personals[i].IdAno);
                    }

                }
            }
            db.SaveChanges();

            Gasto_Ano maxAno = db.Gasto_Ano.Where(r=>r.IdMunicipalidad==gasto.IdMunicipalidad && r.Activo==true).OrderByDescending(r=>r.Ano).First();            
            var suma = db.SP_SumaGastoByIdAno(maxAno.IdAno).First();
            Municipalidad municipalidad = db.Municipalidad.Find(maxAno.IdMunicipalidad);
            switch (maxAno.Semestre)
            {
                case 0:
                    municipalidad.Periodo = "El " + maxAno.Ano +" "; 
                    break;
                case 1:
                    municipalidad.Periodo = "A Marzo de " + maxAno.Ano + " ";
                    break;
                case 2:
                    municipalidad.Periodo = "A Junio de " + maxAno.Ano + " ";
                    break;
                case 3:
                    municipalidad.Periodo = "A Septiembre de " + maxAno.Ano + " ";
                    break;
            }
            municipalidad.TotalGastado = "$"+ Ppuntos(suma.TotalGastado.Value);
            municipalidad.TotalPresupuestado = "$" + Ppuntos(suma.TotalPresupuestado.Value);
            db.SaveChanges();

            return RedirectToAction ("CargaDatosOk");
        }

        public ActionResult CargaDatosOk()
        {
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.administracion = true;
            return View();
        }



        string Ppuntos(long aux2 )
        {
            string aux = Convert.ToString(aux2);
            char[] charArray = aux.ToCharArray();
            Array.Reverse(charArray);
            aux = new string(charArray);


            string puntos = "";
            for (var i=0; i < aux.Length; i++)
            {
                if ((i+1) % 3 == 0 )
                {
                    puntos = puntos + aux[i] + ".";
                }
                else
                {
                    puntos = puntos + aux[i];
                }
            }
            if (puntos[puntos.Length - 1] == '.')
            {
                puntos= puntos.Remove(puntos.Length - 1);
            }

            char[] charArray2 = puntos.ToCharArray();
            Array.Reverse(charArray2);
            return new string(charArray2); ;
        }

        public JsonResult CargadosPost(int aux)
        {
            Gasto_Ano gasto = db.Gasto_Ano.Find(aux);
            bool[] lista = new bool[]
            {
                db.Ingreso_Ano.Any(r=>r.Ano==gasto.Ano && r.Semestre==gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad && r.Cargado==true),
                gasto.Cargado,
                db.Proveedor_Ano.Any(r=>r.Ano==gasto.Ano && r.Semestre==gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad && r.Cargado==true),
                db.Subsidio_Ano.Any(r=>r.Ano==gasto.Ano && r.Semestre==gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad && r.Cargado==true),
                db.Corporacion_Ano.Any(r=>r.Ano==gasto.Ano && r.Semestre==gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad && r.Cargado==true),
                db.Personal_Ano.Any(r=>r.Ano==gasto.Ano && r.Semestre==gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad && r.Cargado==true)
            };
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AbrirPeriodo()
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            List<Gasto_Ano> lista = db.Gasto_Ano.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).ToList();
            List<decimal> final = new List<decimal>();
            int anoactual = DateTime.Now.Year;
            for (var item = 2010; item < anoactual; item++)
            {
                if (!lista.Where(r=>r.Semestre==0).Select(r => r.Ano).Contains(item))
                {
                    final.Add(item);
                }
            }
            if (!lista.Select(r => r.Ano).Contains(anoactual))
            {
                if (!lista.Where(r => r.Ano == anoactual).Select(r => r.Semestre).Contains(0))
                {
                    final.Add(anoactual);
                }
            }
            else
            {
                int mes = DateTime.Now.Month;
                if (mes > 3)
                {
                    if (!lista.Where(r => r.Ano == anoactual).Select(r => r.Semestre).Contains(1))
                    {
                        final.Add(anoactual);
                    }
                }
                if (mes > 6)
                {
                    if (!lista.Where(r => r.Ano == anoactual).Select(r => r.Semestre).Contains(2))
                    {
                        final.Add(anoactual);
                    }
                }
                if (mes > 9)
                {
                    if (!lista.Where(r => r.Ano == anoactual).Select(r => r.Semestre).Contains(3))
                    {
                        final.Add(anoactual);
                    }
                }


            }
            final.Reverse(); // año mas alto.
            decimal ultimoano = final.First();
            List<decimal?> lista2 = lista.Where(r => r.Ano == ultimoano).Select(r => r.Semestre).ToList();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (final.First()== DateTime.Now.Year){
                int mes = DateTime.Now.Month;
                if (!lista2.Contains(1) && mes > 3){
                    dic.Add(1, "a Marzo");
                }
                if (!lista2.Contains(2) && mes > 6){
                    dic.Add(2, "a Junio");
                }
                if (!lista2.Contains(3) && mes > 9){
                    dic.Add(3, "a Septiembre");
                }
            }
            else{
                dic.Add(0, "Completo");
            }
            ViewBag.periodo = new SelectList(dic,"key","value");
            ViewBag.ano = new SelectList(final);
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.idMuni = municipalidad.IdMunicipalidad;
            return View();
        }

        [HttpPost]
        public ActionResult AbrirPeriodo(int periodo, int ano)
        {
            var municipalidad = GetCurrentIdMunicipality();
            Municipalidad muni = db.Municipalidad.Find(municipalidad.IdMunicipalidad);
            ViewBag.idMuni = muni.IdMunicipalidad;
            Corporacion_Ano corp = new Corporacion_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            Personal_Ano pers = new Personal_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            Proveedor_Ano prov = new Proveedor_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            Subsidio_Ano subs = new Subsidio_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            Ingreso_Ano ingr = new Ingreso_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            Gasto_Ano gast = new Gasto_Ano
            {
                IdMunicipalidad = muni.IdMunicipalidad,Ano = ano,UpdatedOn = DateTime.Now,Activo = false,Semestre = periodo,Cargado = false
            };
            muni.Corporacion_Ano.Add(corp);
            muni.Personal_Ano.Add(pers);
            muni.Proveedor_Ano.Add(prov);
            muni.Subsidio_Ano.Add(subs);
            muni.Ingreso_Ano.Add(ingr);
            muni.Gasto_Ano.Add(gast);
            db.Entry(muni).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CargaDatos");
        }

        public JsonResult FiltroMeses(int ano, int idMuni)
        {
            var lista = db.Gasto_Ano.Where(r => r.Ano == ano && r.IdMunicipalidad == idMuni).Select(r => r.Semestre);
            
            List<decimal> final = new List<decimal>();
            int mes = DateTime.Now.Month;
            
            if (!lista.Contains(1) && mes>3)
            {
                final.Add(1);
            }
            if (!lista.Contains(2) && mes > 6)
            {
                final.Add(2);
            }
            if (!lista.Contains(3) && mes > 9)
            {
                final.Add(3);
            }
            return Json(final, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar()
        {
            ViewBag.admimuni = true;
            var tempdata = TempData[errorPassword];
            ViewBag.ErrorPassword = tempdata == null ? "" : tempdata as string;

            var municipalidad = GetCurrentIdMunicipality();
            var invisibles = db.Anos_Invisible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).ToList();
            var visibles = db.Gasto_Ano_Visible.Where(r => r.IdMunicipalidad == municipalidad.IdMunicipalidad).ToList();
            foreach(var item in visibles.ToList())
            {
                Anos_Invisible uno = new Anos_Invisible
                {
                    IdAno = item.IdAno,
                    IdMunicipalidad = item.IdMunicipalidad,
                    Nombre = item.Nombre
                };
                invisibles.Add(uno);
            }
            ViewBag.Anos = new SelectList(invisibles.OrderBy(r=>r.Nombre), "IdAno", "Nombre");
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.administracion = true;
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(int ano, string pass)
        {
            var currentUser = User.Identity.Name;
            var controller = DependencyResolver.Current.GetService<AccountController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            
            var isValidUser = controller.CheckAccount(currentUser, pass);
            if(!isValidUser)
            {
                TempData[errorPassword] = "Contraseña incorrecta";
                return RedirectToAction("Eliminar");
            }

            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.SaveChanges();

            Gasto_Ano gasto = db.Gasto_Ano.Find(ano);

            Municipalidad municipalidad = db.Municipalidad.Find(gasto.IdMunicipalidad);

            



            var gastos = db.Gasto_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();
            var ingresos = db.Ingreso_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();
            var proveedors = db.Proveedor_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();
            var subsidios = db.Subsidio_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();
            var corporacions = db.Corporacion_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();
            var personals = db.Personal_Ano.Where(r => r.Ano == gasto.Ano && r.IdMunicipalidad == gasto.IdMunicipalidad).FirstOrDefault();

            db.SP_DeleteSubsidio(subsidios == null ? 0 : subsidios.IdAno);
            db.SP_DeleteGasto(gastos == null ? 0 : gastos.IdAno);
            db.SP_DeleteIngreso(ingresos == null ? 0 : ingresos.IdAno);
            db.SP_DeleteProveedor(proveedors == null ? 0 : proveedors.IdAno);            
            db.SP_DeleteCorporacion(corporacions == null ? 0 : corporacions.IdAno);
            db.SP_DeletePersonal(personals == null ? 0 : personals.IdAno);
 
            
            db.SaveChanges();

            #region Actualizar Municipalidad
            Gasto_Ano anos = db.Gasto_Ano.Where(r => r.IdMunicipalidad == gasto.IdMunicipalidad).OrderByDescending(r => r.Ano).FirstOrDefault();

            if (anos != null)
            {
                var suma = db.SP_SumaGastoByIdAno(anos.IdAno).First();
                switch (anos.Semestre)
                {
                    case 0:
                        municipalidad.Periodo = "El " + anos.Ano + " ";
                        break;
                    case 1:
                        municipalidad.Periodo = "A Marzo de " + anos.Ano + " ";
                        break;
                    case 2:
                        municipalidad.Periodo = "A Junio de " + anos.Ano + " ";
                        break;
                    case 3:
                        municipalidad.Periodo = "A Septiembre de " + anos.Ano + " ";
                        break;
                }
                municipalidad.TotalGastado = "$" + Ppuntos(suma.TotalGastado.Value);
                municipalidad.TotalPresupuestado = "$" + Ppuntos(suma.TotalPresupuestado.Value);
                db.SaveChanges();
            }
            else
            {
                municipalidad.Periodo = "";
                municipalidad.TotalGastado = "$" + 0;
                municipalidad.TotalPresupuestado = "$" + 0;
                db.SaveChanges();
            }
            #endregion

            
            return RedirectToAction("EliminarDatosOk");
        }

        public ActionResult EliminarDatosOk()
        {
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.Destacado = "hidden";
            ViewBag.administracion = true;
            return View();
        }
        #region Ingresos

        public ActionResult CargaIngresos(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;
            Gasto_Ano gasto = db.Gasto_Ano.Find(id);
            Ingreso_Ano ingr = db.Ingreso_Ano.Single(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad);

            ViewBag.aviso = ingr.Cargado;
            ViewBag.id = ingr.IdAno;

            switch (ingr.Semestre)
            {
                case 1:
                    ViewBag.ano = ingr.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = ingr.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = ingr.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = ingr.Ano;
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CargaIngresos(int id,HttpPostedFileBase fileAdm, HttpPostedFileBase fileSalud, HttpPostedFileBase fileEducacion, HttpPostedFileBase fileCementerio)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (fileAdm == null || fileSalud == null || fileEducacion == null)
            {
                return View();
            }

            XSSFWorkbook xssfwb;
            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;
            var gasto = db.Gasto_Ano.Find(id);
            var ingresoAno = db.Ingreso_Ano.Where(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad).First();                        
            ingresoAno.Cargado = true;
            ingresoAno.UpdatedOn = DateTime.UtcNow;

            LoadReport loadReport = new LoadReport();

            if (fileAdm != null && fileSalud != null && fileEducacion != null)
            {             
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "ADM. Y SERVICIOS", 1);                    
                    SaveBulk(result, "IngresoInformev2");
                }

                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "SALUD", 2);                    
                    SaveBulk(result, "IngresoInformev2");
                }

                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "EDUCACION", 3);                    
                    SaveBulk(result, "IngresoInformev2");
                }

                if (fileCementerio != null)
                {
                    using (Stream fileStream = fileCementerio.InputStream)
                    {
                        xssfwb = new XSSFWorkbook(fileStream);
                        var result = loadReport.LoadInformeIngresov2(xssfwb, "CEMENTERIO", 4);                        
                        SaveBulk(result, "IngresoInformev2");
                    }
                }
            }

            db.SaveChanges();

            var data = db.IngresoInformev2.OrderBy(r => r.IdGroupInformeGasto == loadReport.IdGroupInforme).ToList();
            var dataInforme = Mapper.Map<List<Core.IngresoInformev2>, List<Core.Models.Ingreso.IngresoInforme>>(data);

            var muni = GetCurrentMunicipality().ToLower();
            var csv = StringCSV(dataInforme);
            var blobPath = UploadCSV(muni + "/" +ingresoAno.Ano.ToString() + "/ingreso/dataset.csv", csv);
            ingresoAno.DataFilePath = blobPath;

            db.SP_InformeIngreso(loadReport.IdGroupInforme, ingresoAno.IdAno);

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }

        public ActionResult CargaDiccionarioIngresos()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CargaDiccionarioIngresos(HttpPostedFileBase file)
        {
            bool isValid = true;
            try
            {
                LoadReport loadReport = new LoadReport();
                XSSFWorkbook excelGlosa;
                using (Stream fileStream = file.InputStream)
                {
                    excelGlosa = new XSSFWorkbook(fileStream);
                    var ingresoGlosas = loadReport.LoadIngresoGlosa(excelGlosa);
                    db.Ingreso_Glosa.AddRange(ingresoGlosas);
                    //SaveBulk(ingresoGlosas, "Ingreso_Glosa");
                    var result = await db.SaveChangesAsync();
                }
                return Json(isValid);
            }
            catch (Exception ex)
            {
                return Json(!isValid);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaIngresosAdm()
        {            
            bool isValid = true;
            HttpPostedFileBase fileAdm = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "ADM. Y SERVICIOS", 1);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;                
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaIngresosSalud()
        {
            bool isValid = true;
            HttpPostedFileBase fileSalud = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "SALUD", 2);                    
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaIngresosEducacion()
        {
            bool isValid = true;
            HttpPostedFileBase fileEducacion = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "EDUCACION", 3);                    
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaIngresosCementerio()
        {
            bool isValid = true;
            HttpPostedFileBase fileCementerio = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeIngresov2(xssfwb, "CEMENTERIO", 4);                    
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }      
        }

        #endregion

        #region Gastos

        public ActionResult CargaDiccionarioGastos()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CargaDiccionarioGastos(HttpPostedFileBase file)
        {
            bool isValid = true;
            try
            {
                LoadReport loadReport = new LoadReport();
                XSSFWorkbook excelGlosa;
                using (Stream fileStream = file.InputStream)
                {
                    excelGlosa = new XSSFWorkbook(fileStream);
                    var gastoGlosas = loadReport.LoadGastoGlosa(excelGlosa);
                    db.Gasto_Glosa.AddRange(gastoGlosas);
                    //SaveBulk(gastoGlosas, "Gasto_Glosa");
                    var result = await db.SaveChangesAsync();
                }
                return Json(isValid);
            }
            catch (Exception ex)
            {
                return Json(!isValid);
            }
        }

        public ActionResult CargaGastos(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;
            Gasto_Ano gasto = db.Gasto_Ano.Find(id);

            ViewBag.aviso = gasto.Cargado;
            ViewBag.id = gasto  .IdAno;
            switch (gasto.Semestre)
            {
                case 1:
                    ViewBag.ano = gasto.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = gasto.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = gasto.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = gasto.Ano;
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CargaGastos(int id,HttpPostedFileBase fileAdm, HttpPostedFileBase fileSalud, HttpPostedFileBase fileEducacion, HttpPostedFileBase fileCementerio)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (fileAdm == null || fileSalud == null || fileEducacion == null)
            {
                return View();
            }

            XSSFWorkbook xssfwb;
            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;            
            Gasto_Ano gastoAno = db.Gasto_Ano.Find(id);
            gastoAno.Cargado = true;
            gastoAno.UpdatedOn = DateTime.UtcNow;

            LoadReport loadReport = new LoadReport();

            if (fileAdm != null && fileSalud != null && fileEducacion != null)
            {
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "ADM. Y SERVICIOS", 1);                   
                    SaveBulk(result, "GastoInformev2");
                }

                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "SALUD", 2);                    
                    SaveBulk(result, "GastoInformev2");
                }

                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "EDUCACION", 3);                    
                    SaveBulk(result, "GastoInformev2");
                }

                if (fileCementerio != null)
                {
                    using (Stream fileStream = fileCementerio.InputStream)
                    {
                        xssfwb = new XSSFWorkbook(fileStream);
                        var result = loadReport.LoadInformeGastov2(xssfwb, "CEMENTERIO", 4);                        
                        SaveBulk(result, "GastoInformev2");
                    }
                }
            }

            db.SaveChanges();
            
            var data = db.GastoInformev2.OrderBy(r => r.IdGroupInformeGasto == loadReport.IdGroupInforme).ToList();
            var dataInforme = Mapper.Map<List<Core.GastoInformev2>, List<Core.Models.Gasto.GastoInforme>>(data);

            var muni = GetCurrentMunicipality().ToLower();
            var csv = StringCSV(dataInforme);
            var blobPath = UploadCSV(muni + "/" + gastoAno.Ano.ToString() + "/gasto/dataset.csv", csv);
            gastoAno.DataFilePath = blobPath;

            db.SP_InformeGasto(loadReport.IdGroupInforme, gastoAno.IdAno);

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }
       
        [HttpPost]
        public JsonResult ValidadorGastoIngresosAdm()
        {
            bool isValid = true;
            HttpPostedFileBase fileAdm = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "ADM. Y SERVICIOS", 1);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorGastoIngresosSalud()
        {
            bool isValid = true;
            HttpPostedFileBase fileSalud = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "SALUD", 2);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }
                                   
        [HttpPost]                 
        public JsonResult ValidadorGastoIngresosEducacion()
        {
            bool isValid = true;
            HttpPostedFileBase fileEducacion = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "EDUCACION", 3);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }
                                   
        [HttpPost]                 
        public JsonResult ValidadorGastoIngresosCementerio()
        {
            bool isValid = true;
            HttpPostedFileBase fileCementerio = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeGastov2(xssfwb, "CEMENTERIO", 4);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }
        #endregion

        #region Proveedores
        public ActionResult CargaProveedores(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;

            Gasto_Ano gasto = db.Gasto_Ano.Find(id);
            Proveedor_Ano ingr = db.Proveedor_Ano.First(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad);
            ViewBag.id = ingr.IdAno;

            ViewBag.aviso = ingr.Cargado;
            switch (ingr.Semestre)
            {
                case 1:
                    ViewBag.ano = ingr.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = ingr.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = ingr.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = ingr.Ano;
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CargaProveedores(int id,HttpPostedFileBase fileAdm, HttpPostedFileBase fileSalud, HttpPostedFileBase fileEducacion, HttpPostedFileBase fileCementerio)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (fileAdm == null || fileSalud == null || fileEducacion == null)
            {
                return View();
            }

            var gasto = db.Gasto_Ano.Find(id);
            var proveedorAno = db.Proveedor_Ano.Where(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad).First();
            proveedorAno.Cargado = true;
            proveedorAno.UpdatedOn = DateTime.UtcNow;

            XSSFWorkbook xssfwb;

            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;    
            LoadReport loadReport = new LoadReport();                                           

            if (fileAdm != null)
            {
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);

                    var result = loadReport.LoadInformeProveedoresAdmServicios(xssfwb);                    
                    SaveBulk(result, "Proveedor_AdmInforme");
                    db.SP_ProveedorAdm(loadReport.IdGroupInforme, proveedorAno.IdAno);
                }
            }

            if (fileSalud != null)
            {
                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresSalud(xssfwb);                    
                    SaveBulk(result, "Proveedor_SaludInforme");
                    db.SP_ProveedorSalud(loadReport.IdGroupInforme, proveedorAno.IdAno);
                }
            }

            if (fileEducacion != null)
            {
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresEducacion(xssfwb);                    
                    SaveBulk(result, "Proveedor_EducacionInforme");
                    db.SP_ProveedorEducacion(loadReport.IdGroupInforme, proveedorAno.IdAno);
                }
            }

            if (fileCementerio != null)
            {
                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresCementerio(xssfwb);                    
                    SaveBulk(result, "Proveedor_CementerioInforme");
                    db.SP_ProveedorCementerio(loadReport.IdGroupInforme, proveedorAno.IdAno);
                }

            }

            db.SaveChanges();

            var data = db.Proveedor_AdmInforme.OrderBy(r => r.IdGroupInformeProveedores == loadReport.IdGroupInforme).ToList();
            var data1 = db.Proveedor_CementerioInforme.OrderBy(r => r.IdGroupInformeProveedores == loadReport.IdGroupInforme).ToList();
            var data2 = db.Proveedor_EducacionInforme.OrderBy(r => r.IdGroupInformeProveedores == loadReport.IdGroupInforme).ToList();
            var data3 = db.Proveedor_SaludInforme.OrderBy(r => r.IdGroupInformeProveedores == loadReport.IdGroupInforme).ToList();

            
            var dataInforme = Mapper.Map<List<Core.Proveedor_AdmInforme>, List<Core.Models.Proveedor.ProveedorInforme>>(data);
            var dataInforme1 = Mapper.Map<List<Core.Proveedor_CementerioInforme>, List<Core.Models.Proveedor.ProveedorInforme>>(data1);
            var dataInforme2 = Mapper.Map<List<Core.Proveedor_EducacionInforme>, List<Core.Models.Proveedor.ProveedorInforme>>(data2);
            var dataInforme3 = Mapper.Map<List<Core.Proveedor_SaludInforme>, List<Core.Models.Proveedor.ProveedorInforme>>(data3);

            dataInforme.AddRange(dataInforme1);
            dataInforme.AddRange(dataInforme2);
            dataInforme.AddRange(dataInforme3);

            var muni = GetCurrentMunicipality().ToLower();
            var csv = StringCSV(dataInforme);

            var blobPath = UploadCSV(muni + "/" + proveedorAno.Ano.ToString() + "/proveedor/dataset.csv", csv);
            proveedorAno.DataFilePath = blobPath;

            db.SP_ProveedorTotal(loadReport.IdGroupInforme, proveedorAno.IdAno);

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }

        [HttpPost]
        public JsonResult ValidadorCargaProveedoresAdm()
        {
            bool isValid = true;
            HttpPostedFileBase fileAdm = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresAdmServicios(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaProveedoresSalud()
        {
            bool isValid = true;
            HttpPostedFileBase fileSalud = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();

                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresSalud(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaProveedoresEducacion()
        {
            bool isValid = true;
            HttpPostedFileBase fileEducacion = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresEducacion(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaProveedoresCementerio()
        {
            bool isValid = true;
            HttpPostedFileBase fileCementerio = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformeProveedoresCementerio(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }
        #endregion

        #region Subsidios

        public ActionResult CargaSubsidios(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;
            Gasto_Ano gasto = db.Gasto_Ano.Find(id);
            Subsidio_Ano ingr = db.Subsidio_Ano.First(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad);
            ViewBag.aviso = ingr.Cargado;
            ViewBag.id = ingr.IdAno;

            switch (ingr.Semestre)
            {
                case 1:
                    ViewBag.ano = ingr.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = ingr.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = ingr.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = ingr.Ano;
                    break;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidadorCargaSubsidios()
        {
            bool isValid = true;
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                using (Stream fileStream = file.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    LoadReport loadReport = new LoadReport();
                    var result = loadReport.LoadInformeSubsidio(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public ActionResult CargaSubsidios(int id,HttpPostedFileBase file)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (file == null)
            {
                return View();
            }

            var gasto = db.Gasto_Ano.Find(id);
            var subsidioAno = db.Subsidio_Ano.Where(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad).First();
            subsidioAno.Cargado = true;
            subsidioAno.UpdatedOn = DateTime.UtcNow;

            XSSFWorkbook xssfwb;
            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;            
            
            using (Stream fileStream = file.InputStream)
            {
                xssfwb = new XSSFWorkbook(fileStream);
                LoadReport loadReport = new LoadReport();
                var result = loadReport.LoadInformeSubsidio(xssfwb);                
                SaveBulk(result, "SubsidioInforme");

                var data = db.SubsidioInforme.OrderBy(r => r.IdGroupInformeSubsidio == loadReport.IdGroupInforme).ToList();
                var dataInforme = Mapper.Map<List<Core.SubsidioInforme>, List<Core.Models.Subsidio.SubsidioInforme>>(data);

                var muni = GetCurrentMunicipality().ToLower();
                var csv = StringCSV(dataInforme);
                var blobPath = UploadCSV(muni + "/" + subsidioAno.Ano.ToString() + "/subsidio/dataset.csv", csv);
                subsidioAno.DataFilePath = blobPath;

                db.SP_InformeSubsidio(loadReport.IdGroupInforme, subsidioAno.IdAno);
            }

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }
        #endregion

        #region Corporaciones
        public ActionResult CargaCorporaciones(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;
            Gasto_Ano gasto = db.Gasto_Ano.Find(id);
            Corporacion_Ano ingr = db.Corporacion_Ano.First(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad);
            ViewBag.aviso = ingr.Cargado;
            ViewBag.id = ingr.IdAno;

            switch (ingr.Semestre)
            {
                case 1:
                    ViewBag.ano = ingr.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = ingr.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = ingr.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = ingr.Ano;
                    break;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidadorCargaCorporaciones()
        {
            bool isValid = true;
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                using (Stream fileStream = file.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    LoadReport loadReport = new LoadReport();
                    var result = loadReport.LoadInformeCorporaciones(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public ActionResult CargaCorporaciones(int id,HttpPostedFileBase file)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (file == null)
            {
                return View();
            }

            var gasto = db.Gasto_Ano.Find(id);
            var corporacionAno = db.Corporacion_Ano.Where(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad).First();
            corporacionAno.Cargado = true;
            corporacionAno.UpdatedOn = DateTime.UtcNow;

            XSSFWorkbook xssfwb;

            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;         

            using (Stream fileStream = file.InputStream)
            {
                xssfwb = new XSSFWorkbook(fileStream);
                LoadReport loadReport = new LoadReport();
                var result = loadReport.LoadInformeCorporaciones(xssfwb);                
                SaveBulk(result, "CorporacionInforme");
                
                var data = db.CorporacionInforme.OrderBy(r => r.IdGroupInforme == loadReport.IdGroupInforme).ToList();
                var dataInforme = Mapper.Map<List<Core.CorporacionInforme>, List<Core.Models.Corporacion.CorporacionInforme>>(data);

                var muni = GetCurrentMunicipality().ToLower();
                var csv = StringCSV(dataInforme);
                var blobPath = UploadCSV(muni + "/" + corporacionAno.Ano.ToString() + "/corporacion/dataset.csv", csv);
                corporacionAno.DataFilePath = blobPath;

                db.SP_InformeCorporaciones(loadReport.IdGroupInforme, corporacionAno.IdAno);
            }

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }
        #endregion

        #region Remuneraciones
        public ActionResult CargaRemuneraciones(int id)
        {
            ViewBag.admimuni = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = municipalidad.DireccionWeb + ".png";
            ViewBag.cementerio = municipalidad.Cementerio;
                                    
            Gasto_Ano gasto = db.Gasto_Ano.Find(id);
            Personal_Ano ingr = db.Personal_Ano.First(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad);
            ViewBag.aviso = ingr.Cargado;
            ViewBag.id = ingr.IdAno;

            switch (ingr.Semestre)
            {
                case 1:
                    ViewBag.ano = ingr.Ano + " a marzo";
                    break;
                case 2:
                    ViewBag.ano = ingr.Ano + " a junio";
                    break;
                case 3:
                    ViewBag.ano = ingr.Ano + " a septiembre";
                    break;
                default:
                    ViewBag.ano = ingr.Ano;
                    break;
            }
            return View();            
        }

        [HttpPost]
        public JsonResult ValidadorCargaRemuneracionesAdm()
        {
            bool isValid = true;
            HttpPostedFileBase fileAdm = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalAdmServicios(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaRemuneracionesSalud()
        {
            bool isValid = true;
            HttpPostedFileBase fileSalud = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();

                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalSalud(xssfwb);
                }

                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public JsonResult ValidadorCargaRemuneracionesEducacion()
        {
            bool isValid = true;
            HttpPostedFileBase fileEducacion = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalEducacion(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public ActionResult ValidadorCargaRemuneracionesCementerio()
        {
            bool isValid = true;
            HttpPostedFileBase fileCementerio = Request.Files[0];
            try
            {
                XSSFWorkbook xssfwb;
                LoadReport loadReport = new LoadReport();

                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalCementerio(xssfwb);
                }
                var resultJson = new { isValid = isValid, message = okMessage };
                return Json(resultJson);
            }
            catch (Exception ex)
            {
                var position = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().IndexOf("GetCell");
                position = position + 8;
                var columnPositionString = ((AutoMapper.AutoMapperMappingException)ex).PropertyMap.CustomExpression.ToString().Substring(position, 1);
                var columnPositionint = 0;
                int.TryParse(columnPositionString, out columnPositionint);
                columnPositionint++;
                var message = errorMessage + columnPositionint;

                var resultJson = new { isValid = !isValid, message = message };
                return Json(resultJson);
            }
        }

        [HttpPost]
        public ActionResult CargaRemuneraciones(int id, HttpPostedFileBase fileAdm, HttpPostedFileBase fileSalud, HttpPostedFileBase fileEducacion, HttpPostedFileBase fileCementerio)
        {
            var timeout = db.Database.CommandTimeout;
            db.Database.CommandTimeout = 2400;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.SaveChanges();

            if (fileAdm == null || fileSalud == null || fileEducacion == null)
            {
                return View();
            }

            int idMunicipality = GetCurrentIdMunicipality().IdMunicipalidad;
            var gasto = db.Gasto_Ano.Find(id);
            var personalAno = db.Personal_Ano.Where(r => r.Ano == gasto.Ano && r.Semestre == gasto.Semestre && r.IdMunicipalidad == gasto.IdMunicipalidad).First();
            personalAno.Cargado = true;
            personalAno.UpdatedOn = DateTime.UtcNow;

            XSSFWorkbook xssfwb;

            LoadReport loadReport = new LoadReport();
            if (fileAdm != null)
            {
                using (Stream fileStream = fileAdm.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);

                    var result = loadReport.LoadInformePersonalAdmServicios(xssfwb);                    
                    SaveBulk(result, "Personal_AdmInforme");
                    db.SP_InformePersonalAdm(loadReport.IdGroupInforme, personalAno.IdAno);
                }
            }

            if (fileSalud != null)
            {
                using (Stream fileStream = fileSalud.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalSalud(xssfwb);                    
                    SaveBulk(result, "Personal_SaludInforme");
                    db.SP_InformePersonalSalud(loadReport.IdGroupInforme, personalAno.IdAno);
                }
            }

            if (fileEducacion != null)
            {
                using (Stream fileStream = fileEducacion.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalEducacion(xssfwb);                    
                    SaveBulk(result, "Personal_EducacionInforme");
                    db.SP_InformePersonalEducacion(loadReport.IdGroupInforme, personalAno.IdAno);
                }
            }

            if (fileCementerio != null)
            {
                using (Stream fileStream = fileCementerio.InputStream)
                {
                    xssfwb = new XSSFWorkbook(fileStream);
                    var result = loadReport.LoadInformePersonalCementerio(xssfwb);                    
                    SaveBulk(result, "Personal_CementerioInforme");
                    db.SP_InformePersonalCementerio(loadReport.IdGroupInforme, personalAno.IdAno);
                }
            }
            var data = db.Personal_AdmInforme.OrderBy(r => r.IdGroupInformePersonal == loadReport.IdGroupInforme).ToList();
            var data1 = db.Personal_CementerioInforme.OrderBy(r => r.IdGroupInformePersonal == loadReport.IdGroupInforme).ToList();
            var data2 = db.Personal_EducacionInforme.OrderBy(r => r.IdGroupInformePersonal == loadReport.IdGroupInforme).ToList();
            var data3 = db.Personal_SaludInforme.OrderBy(r => r.IdGroupInformePersonal == loadReport.IdGroupInforme).ToList();
            
            var dataInforme = Mapper.Map<List<Core.Personal_AdmInforme>, List<Core.Models.Personal.PersonalInforme>>(data);
            var dataInforme1 = Mapper.Map<List<Core.Personal_CementerioInforme>, List<Core.Models.Personal.PersonalInforme>>(data1);
            var dataInforme2 = Mapper.Map<List<Core.Personal_EducacionInforme>, List<Core.Models.Personal.PersonalInforme>>(data2);
            var dataInforme3 = Mapper.Map<List<Core.Personal_SaludInforme>, List<Core.Models.Personal.PersonalInforme>>(data3);

            dataInforme.AddRange(dataInforme1);
            dataInforme.AddRange(dataInforme2);
            dataInforme.AddRange(dataInforme3);

            var muni = GetCurrentMunicipality().ToLower();
            var csv = StringCSV(dataInforme);
            var blobPath = UploadCSV(muni + "/" + personalAno.Ano.ToString() + "/remuneracion/dataset.csv", csv);
            personalAno.DataFilePath = blobPath;

            //db.SP_InformePersonalMunicipioTotal(loadReport.IdGroupInforme, personalAno.IdAno);

            db.ChangeTracker.DetectChanges();
            db.SaveChanges();

            return RedirectToAction("CargaDatos");
        }
       
        #endregion

    }

}
