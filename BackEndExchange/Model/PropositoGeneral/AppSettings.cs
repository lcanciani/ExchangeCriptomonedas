using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.PropositoGeneral
{
    public class AppSettings
    {
        public string secreto { get; set; }
        public Cadena connectionString { get; set; }
    }


    public class Cadena
    {
        public string cadenaConexion;
    }
}
