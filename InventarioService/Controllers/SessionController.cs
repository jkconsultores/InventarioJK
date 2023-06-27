using InventarioService.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model_Inventario.InventarioDTO;
using Repositorio_Inventario;

namespace InventarioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ICustomAuthenticationManagerService managerService;
        private readonly SqlDbContext contexto;
        public SessionController(ICustomAuthenticationManagerService managerService, SqlDbContext contexto)
        {
            this.managerService = managerService;
            this.contexto = contexto;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] UsuarioLoginDTO UserCredential)
        {
            try
            {
                return Ok(managerService.Authenticate(UserCredential, contexto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
