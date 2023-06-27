using InventarioService.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using Model_Inventario.InventarioDTO.Error;
using Model_Inventario.InventarioDTO;
using Repositorio_Inventario;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Repositorio_Inventario.Repository.Implementacion;

namespace InventarioService.Services.Implementacion
{
    public class CustomAuthenticationManagerServiceImpl : ICustomAuthenticationManagerService
    {
        private readonly IDictionary<string, string> tokens = new Dictionary<string, string>();
        public IDictionary<string, string> Tokens => tokens;

        protected CustomAuthenticationManagerRepositorioPortal _CustomAuthenticationManagerRepositorioPortal;

        private readonly string tokenKey;


        public CustomAuthenticationManagerServiceImpl(string tokenKey)
        {
            this.tokenKey = tokenKey;
            _CustomAuthenticationManagerRepositorioPortal = new CustomAuthenticationManagerRepositorioPortal();
        }

        public UsuariosReturnPortalDTO Authenticate(UsuarioLoginDTO usuarioLog, SqlDbContext context)
        {
            //if (!users.Any(u => u.Key == username && u.Value == password))
            //{
            //    return null;
            //}

            UsuariosReturnPortalDTO _registroAuntenticate = new UsuariosReturnPortalDTO();

            var emrpesa = _CustomAuthenticationManagerRepositorioPortal.ObtenerEmpresa(usuarioLog, context);
            if (emrpesa == null)
            {
                throw new ExceptionDTO()
                {
                    error = new ErrorDetail
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DetailError = "No es un error critico, este error es por una empresa inexistente",
                        CodError = "USU-Ex000001",
                        Message = "No se encontro la empresa."
                    }
                };
            }

            var resultadoAutenticacion = _CustomAuthenticationManagerRepositorioPortal.AutenticacionUsuarioPortal(usuarioLog, emrpesa);

            if (resultadoAutenticacion == null)
            {
                throw new ExceptionDTO()
                {
                    error = new ErrorDetail
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        DetailError = "No es un error critico, este error es por un usuario inexistente",
                        CodError = "USU-Ex000002",
                        Message = "Usuario o clave incorrectos, vuelva a ingresar los datos correctamente."
                    }
                };

            }

            var ecKeyTemp = Encoding.ASCII.GetBytes(tokenKey);

            // Note that the ecKey should have 256 / 8 length:
            byte[] ecKey = new byte[256 / 8];
            DateTime fechaExpiracion = DateTime.UtcNow.AddDays(30);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, usuarioLog.nombreusuario),
                    new Claim("Id", resultadoAutenticacion.Id.ToString()),
                    new Claim("Usuario", resultadoAutenticacion.Usuario.ToString()),
                    new Claim("Empresa", emrpesa.descripcion.ToString()),
                    new Claim("Expira", fechaExpiracion.ToString()),
                    new Claim("Rol", resultadoAutenticacion.Rol.ToString()),
                    new Claim("Correo", resultadoAutenticacion.correo.ToString()),
                    new Claim("BD_Sql",emrpesa.cadenaconexion.ToString())
               }),
                Expires = fechaExpiracion,
                SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(key),
                   SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(

                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                     SecurityAlgorithms.Aes128KW,
                     SecurityAlgorithms.Aes128CbcHmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            resultadoAutenticacion.Token = tokenHandler.WriteToken(token);

            _registroAuntenticate.Usuario = resultadoAutenticacion.Usuario;
            _registroAuntenticate.Id = resultadoAutenticacion.Id;
            _registroAuntenticate.Empresa = emrpesa.descripcion;
            _registroAuntenticate.Expira = fechaExpiracion;
            _registroAuntenticate.Token = resultadoAutenticacion.Token;
            return _registroAuntenticate;
        }
    }
}
