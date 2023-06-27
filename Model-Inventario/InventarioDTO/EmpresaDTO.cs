using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.InventarioDTO
{
    public class EmpresaDTO
    {
        public string? descripcion { get; set; }
        public int ruc { get; set; }
        public string? direccion { get; set; }
        public int telefono { get; set; }
        public string? cadenaconexion { get; set; }
        public int grupo { get; set; }
        public string? app { get; set; }
    }
}
