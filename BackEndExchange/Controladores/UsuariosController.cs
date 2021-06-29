using BackEndExchange.Model;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using BackEndExchange.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosController : ControllerBase
    {
        private ExchangeDBContext _ex;


        
        public UsuariosController(ExchangeDBContext ex)
        {
            _ex = ex;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _ex.Usuarios.ToList();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("{id}")]

        //Probar si trar todos los registros que estan en el arreglo "id"
        public IActionResult Get(int[] id)
        {
            try
            {
                
                var usuarios = _ex.Usuarios.Find(id);
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("rango")]
        public IActionResult Post(object[] id)
        {
            try
            {
                
                var usuarios = _ex.Usuarios.Find(id);
                return Ok(usuarios);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }   
        }

        //Metodo POST para Agregar un usuarios/ add an user
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioModel model)
        {
            try
            {
                Usuario u = new Usuario();
                u.IdUsuario = model.IdUsuario;

                _ex.Usuarios.Add(u);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel model)
        {
            Respuesta resp = new Respuesta();
            try
            {

                var usuarioModificar = _ex.Usuarios.Find(id);
                if (usuarioModificar == null)
                {
                    resp.exito = 0;
                    resp.mensaje = "El usuario a modificar no existe";
                    return BadRequest(resp);
                }
                usuarioModificar.Nombre = model.Nombre;
                usuarioModificar.Apellido = model.Apellido;
                usuarioModificar.Direccion = model.Direccion;
                usuarioModificar.Email = model.Email;
                usuarioModificar.SaldoFiat = model.SaldoFiat;
                usuarioModificar.ClavePublica = model.ClavePublica;
                usuarioModificar.ClavePrivada = model.ClavePrivada;
                usuarioModificar.Contrasenia = model.Contrasenia;
                
                _ex.Usuarios.Update(usuarioModificar);
                _ex.SaveChanges();
                resp.exito = 1;
                resp.mensaje = "El usuario se modifico correctamente";
                return Ok(resp);
            }
            catch (Exception e)
            {
                resp.exito = 0;
                resp.mensaje = "No se pudo modificar el usuario";
                resp.Exception = e.Message;

                return BadRequest(resp);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                Usuario u = new Usuario();
                u = _ex.Usuarios.Find(id);
                if (u == null)
                    return BadRequest("El Usuario que intenta eliminar no existe");

                _ex.Usuarios.Remove(u);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
