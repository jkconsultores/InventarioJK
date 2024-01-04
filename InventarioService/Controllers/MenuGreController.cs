using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios_Inventario.Service.Interface;

namespace ValidarEstadoDocumentosSunat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuGreController : ControllerBase
    {
        private readonly IMenuGreService menuGreService;

        public MenuGreController(IMenuGreService menuGreService)
        {
            this.menuGreService = menuGreService;
        }

        [HttpGet]
        public IActionResult ObtnerMenu()
        {
            try
            {
                var BD_Sql = User.Claims.FirstOrDefault(x => x.Type == "BD_Sql").Value;
                var IdEmpresa = User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                return Ok(menuGreService.ObtenerMenuPorEmpresa(IdEmpresa, BD_Sql));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
