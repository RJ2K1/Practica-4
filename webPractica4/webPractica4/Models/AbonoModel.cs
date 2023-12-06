using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webPractica4.Models
{
    public class AbonoModel
    {
        public int id { get; set; }
        public Nullable<int> codCompra { get; set; }
        public Nullable<decimal> abono { get; set; }
    }
}