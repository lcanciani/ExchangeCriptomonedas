using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Response
{
    public class UserResponse
    {
    public int idUsuario { get; set; }
        public string email { get; set; }

        public string token { get; set; }
    }
}
