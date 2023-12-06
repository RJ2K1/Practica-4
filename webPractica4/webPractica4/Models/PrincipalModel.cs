using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webPractica4.Models
{
    public class PrincipalModel
    {
        public int codCompra { get; set; }
        public string descripcion { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public string estado { get; set; }
    }
}