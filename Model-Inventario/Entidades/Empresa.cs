using Model_Inventario.Entidades.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.Entidades
{
    public class Empresa : BaseEntity
    {
        [MaxLength(50)]
        public string? descripcion { get; set; }
        public int ruc { get; set; }
        [MaxLength(100)]
        public string? direccion { get; set; }
        public int telefono { get; set; }
        [MaxLength(120)]
        public string? cadenaconexion { get; set; }
        public int grupo { get; set; }
        public string? app { get; set; }
    }
}
