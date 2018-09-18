using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastoTransparenteMunicipal.Models
{
    public class ServiceStatus
    {
        public bool DataBase { get; set; }
        public bool Email { get; set; }
        public bool BlobService { get; set; }
    }
}