using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using BackEndExchange.Services;
using BackEndExchange.Tools;
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
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var usuarios = _ex.Usuarios.ToList();
        List<UsuarioModel> lum = new List<UsuarioModel>();
        foreach(Usuario u in usuarios)
        {
          UsuarioModel um = new UsuarioModel();
          um.IdUsuario = u.IdUsuario;
          um.Nombre = u.Nombre.Trim();
          um.Apellido = u.Apellido.Trim();
          um.Direccion = u.Direccion.Trim();
          um.Email = u.Email.Trim();
          um.Contrasenia = u.Contrasenia.Trim();
          um.FechaBaja = u.FechaBaja;
          if(u.Dni != null)
          {
            um.Dni = u.Dni.Trim();
          }
          
          um.SaldoFiatUsuario = u.SaldoFiatUsuario;
          lum.Add(um);
        }
        rm.exito = 1;
        rm.data = lum;
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = "No se pudo obtener los usuarios";
        rm.data = e.Message;
      return Ok(rm);

      }
    }

    [HttpGet("{id}")]

    public IActionResult Get(int id)
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var saldoUsuario = _ex.Usuarios.Find(id).SaldoFiatUsuario;
        if(saldoUsuario == null)
        {
          rm.exito = 0;
          rm.mensanje = "El usuario no existe";
          return Ok(rm);
        }

        rm.exito = 1;
        rm.data = saldoUsuario;
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
      
    }
      //Metodo POST para Agregar un usuarios/ add an user
      [HttpPost]
        public IActionResult Post([FromBody] RegistrarUsuarioModel model)
        {
      RespuestaModel rm = new RespuestaModel();
      using(var registrarUsuario = _ex.Database.BeginTransaction())
      {

      
            try
            {
                Usuario u = new Usuario();
        u.Nombre = model.Nombre;
        u.Apellido = model.Apellido;
        u.Email = model.Email;
        u.Direccion = model.Direccion;
        u.Dni = model.Dni;
          u.SaldoFiatUsuario = 0;
          
        u.Contrasenia = Sha256.GetSHA256( model.Password);
                _ex.Usuarios.Add(u);
        _ex.SaveChanges();
        foreach(BancoCbu bc in model.BancoCbu)
        {
          BancosUsuario bu = new BancosUsuario();
          bu.IdBanco = bc.IdBanco;
          bu.Cbu = bc.Cbu;
          bu.IdUsuario = u.IdUsuario;
          _ex.BancosUsuarios.Add(bu);
          _ex.SaveChanges();
        }
          registrarUsuario.Commit();
          rm.exito = 1;
          rm.mensanje = "Usuario registrado con éxito!";
          return Ok(rm);
            }
        
            catch (Exception e)
            {
          registrarUsuario.Rollback();
        rm.exito = 0;
        rm.mensanje = e.Message;
                return Ok(rm);
            }
      }
    }

       

    }
}
