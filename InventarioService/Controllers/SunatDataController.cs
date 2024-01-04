using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Inventario.Entidades;
using Servicios_Inventario.Service.Interface;
using ValidarEstadoDocumentosSunat.Cipher;

namespace ValidarEstadoDocumentosSunat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SunatDataController : ControllerBase
    {
        private readonly ISunatDataService sunatDataService;
        private readonly IConfiguration configuration;

        public SunatDataController(ISunatDataService sunatDataService, IConfiguration configuration)
        {
            this.sunatDataService = sunatDataService;
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult AgregarCredenciales([FromBody] SunatData data) {
            try
            {
                var salt = configuration.GetValue<string>("TokenKey");
                var BD_Sql = User.Claims.FirstOrDefault(x => x.Type == "BD_Sql").Value;
                data.Client_secret = CifradoPrimerNivel.Encrypt(data.Client_secret, "JkSmartData" + salt);
                data.Client_id = CifradoPrimerNivel.Encrypt(data.Client_id, "JkSmartData" + salt);
                return Ok(sunatDataService.AddCredentials(data, BD_Sql));
            }catch (Exception ex) { return BadRequest(ex.Message); }    
        }
    }
}
