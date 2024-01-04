using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.Entidades
{
    public class DesdeHastaDTO
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int inicio { get; set; }
        public int cantidad { get; set; }
        public string? serieNumero { get; set; }
        public string? rucAdquiriente { get; set; }
        public string? estadoCp { get; set; }
    }
}
