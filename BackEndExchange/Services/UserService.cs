using BackEndExchange.Model;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndExchange.Tools;
using BackEndExchange.Model.PropositoGeneral;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BackEndExchange.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse respuesta = new UserResponse();
            using(ExchangeDBContext db = new ExchangeDBContext()) 
            { 
                string sPassword = Sha256.GetSHA256(model.password);
                var usuario = db.Usuarios.Where(d => d.Email == model.email && d.Contrasenia == sPassword).FirstOrDefault();

                if (usuario == null) return null;
                respuesta.email = usuario.Email;
                respuesta.token = getToken(usuario);
            }

            return respuesta;
        }

        private string getToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
