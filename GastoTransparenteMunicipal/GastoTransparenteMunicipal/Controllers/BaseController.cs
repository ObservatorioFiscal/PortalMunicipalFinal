using Core;
using GastoTransparenteMunicipal.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Web.Mvc;

namespace GastoTransparenteMunicipal.Controllers
{
    public class BaseController : Controller
    {
        //private string connectionString = "Server=tcp:serverobservatorio.database.windows.net,1433;Initial Catalog=GastoTransparenteMunicipal;Persist Security Info=False;User ID=adminserverprueba;Password=Observatori02016;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private readonly string connectionString = ConfigurationManager.AppSettings["ConexionBddBulk"];

        public GastoTransparenteMunicipalEntities db = new GastoTransparenteMunicipalEntities();        
        
        public void IncreaseTimeOutDb()
        {
            var timeout = db.Database.CommandTimeout;
            this.db.Database.CommandTimeout = 2400;
            this.db.SaveChanges();
        }

        public string GetAccountKey
        {
            get
            {
                var key = ConfigurationManager.AppSettings["AccountKey"];
                return key;
            }
        }

        public string GetAccountName
        {
            get
            {
                var name = ConfigurationManager.AppSettings["AccountName"];
                return name;
            }
        }
        public string GetCurrentMunicipality()
        {
            var municipalityName = RouteData.Values["municipality"].ToString();
            return municipalityName;
        }

        public Municipalidad GetCurrentIdMunicipality()
        {
            try
            {
                var municipalityName = RouteData.Values["municipality"].ToString();
                var municipality = db.Municipalidad.SingleOrDefault(r => r.DireccionWeb == municipalityName);
                return municipality;
            }
            catch
            {
                return null;
            }
        }

        public int SaveBulk<T>(List<T> list,string tableName)
        {            
            ConvertTo convert = new ConvertTo();
            using (DataTable table = convert.DataTable<T>(list))
            {
                using (var sqlBulk = new SqlBulkCopy(connectionString))
                {
                    sqlBulk.DestinationTableName = tableName;
                    sqlBulk.WriteToServer(table);
                    return 1;
                }
            }
        }
    }
}