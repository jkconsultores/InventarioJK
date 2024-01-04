using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Model_Inventario.SunatValidationDTO;
using ValidarEstadoDocumentosSunat.Cipher;
using ValidarEstadoDocumentosSunat.Services.Interface;

namespace ValidarEstadoDocumentosSunat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValidationController : ControllerBase
    {
        private readonly ISunatService sunatService;
        private readonly IConfiguration configuration;

        public ValidationController(ISunatService sunatService, IConfiguration configuration)
        {
            this.sunatService = sunatService;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("document")]
        public IActionResult ValidationComponent([FromBody] DocumentDTO docuemnto)
        {
            try
            {
                var salt = configuration.GetValue<string>("TokenKey");
                var clientdi = User.Claims.FirstOrDefault(x => x.Type == "Client_id").Value;
                var ClientId = CifradoPrimerNivel.Decrypt(clientdi, "JkSmartData" + salt);
                var ClientSecret = CifradoPrimerNivel.Decrypt(User.Claims.FirstOrDefault(x => x.Type == "Client_secret").Value, "JkSmartData" + salt);
                var BD_Sql = User.Claims.FirstOrDefault(x => x.Type == "BD_Sql").Value;
                LogeoSunat log = new LogeoSunat(ClientId, ClientSecret);
                var dato = sunatService.ValidacionDelComprobante(docuemnto, log, BD_Sql);
                return Ok(dato);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        [Route("all/documents")]
        public async Task<IActionResult> ValidationComponent()
        {
            try
            {
                var salt = configuration.GetValue<string>("TokenKey");
                var ClientId = CifradoPrimerNivel.Decrypt(User.Claims.FirstOrDefault(x => x.Type == "Client_id").Value,"JkSmartData"+ salt);
                var ClientSecret = CifradoPrimerNivel.Decrypt(User.Claims.FirstOrDefault(x => x.Type == "Client_secret").Value, "JkSmartData"+ salt);
                LogeoSunat log = new LogeoSunat(ClientId, ClientSecret);
                var BD_Sql = User.Claims.FirstOrDefault(x => x.Type == "BD_Sql").Value;
                 var adtos = await sunatService.ValidacionDelComprobanteASync(log,BD_Sql);
                return Ok(new {mensaje= "Se termino la validacion" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        [Route("desde")]
        public IActionResult ObtenerData([FromBody] DesdeHastaDTO Data)
        {
            try
            {
                var BD_Sql = User.Claims.FirstOrDefault(x => x.Type == "BD_Sql").Value;
                return Ok(sunatService.ObtenerDocs(Data,BD_Sql));

            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
