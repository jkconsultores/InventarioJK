using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios_Inventario.Service.Implementacion;
using Servicios_Inventario.Service.Interface;

namespace InventarioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            this.empresaService = empresaService;
        }
        [HttpGet]
        public IActionResult ObtnerEmrpeas()
        {
            try
            {
                return Ok(empresaService.ObtenerEmpresa());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
