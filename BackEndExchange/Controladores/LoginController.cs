using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using BackEndExchange.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;


        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]


        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                respuesta.exito = 0;
                respuesta.mensaje = "Usuario o contraseña incorrecto";
                return Ok(respuesta);

            }

            respuesta.exito = 1;
            respuesta.mensaje = "Exito!";
            respuesta.data = userResponse;

            return Ok(respuesta);
        }


    }
}
