using Model_Inventario.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.Entidades
{
    public class T_MenuGre : BaseEntity
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
