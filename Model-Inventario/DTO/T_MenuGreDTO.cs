using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.DTO
{
    public class T_MenuGreDTO
    {
        public int Id { get; set; }
        public string Ruc { get; set; }

        public bool Estado { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public string Icono { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string orden { get; set; }
    }
    public class T_MenuGreCreateDTO
    {
        public string Ruc { get; set; }

        public bool Estado { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public string Icono { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string orden { get; set; }
    }
}
