using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Model_Inventario.SunatValidationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Interface
{
    public interface ITDocumentosAValidarRepository
    {
        List<DocumentosAValidar> ObtenerTodasLasValidacionesPendientes(string db);
        pagination obtenerReporte(DesdeHastaDTO data, string db);
        void agregarData(DocumentosAValidar data, string db);
        void aActualziarData(DocumentosAValidar data, string db);
    }
}
