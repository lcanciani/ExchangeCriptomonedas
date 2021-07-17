using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Response
{
    public class Respuesta
    {
    public int exito { get; set; }
    public string mensaje { get; set; }
        

        
        public UserResponse data { get; set; }
    }
}
