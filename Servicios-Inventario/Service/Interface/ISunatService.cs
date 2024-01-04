using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Model_Inventario.SunatValidationDTO;

namespace ValidarEstadoDocumentosSunat.Services.Interface
{
    public interface ISunatService
    {
        object ValidacionDelComprobante(DocumentDTO docuemnto, LogeoSunat log, string db);
        Task<object> ValidacionDelComprobanteASync(LogeoSunat log,string db);
        pagination ObtenerDocs(DesdeHastaDTO desdeshasta, string db);
    }
}
