using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.SunatValidationDTO
{
    public class DocumentosAValidarDTO
    {
        public string tipoDocumentoRemision { get; set; }

        public string numeroDocumentoRemision { get; set; }

        public string serieNumero { get; set; }

        public string tipoDocumento { get; set; }

        public DateTime FechaEmision { get; set; }

        public decimal MontoTotal { get; set; }

        public bool Procesado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string NombreUsuario { get; set; }

        public DateTime? FechaDeConsulta { get; set; }

        public string? EstadoCp { get; set; }

        public string? estadoRuc { get; set; }

        public string? condDomiRuc { get; set; }

        public string? Mensaje { get; set; }

        public bool? EstadoDoc { get; set; }

        public string? Observaciones { get; set; }
    }
    public class pagination
    {
        public int TotalRegistros { get; set; }
        public List<DocumentosAValidarDTO>? Datos { get; set; }
    }
}
