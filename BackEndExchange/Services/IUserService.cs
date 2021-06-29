using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);

    }
}
