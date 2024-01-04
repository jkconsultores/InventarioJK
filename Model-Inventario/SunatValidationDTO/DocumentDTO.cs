using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.InventarioDTO
{
    public class DocumentDTO
    {
        public string numRuc { get; set; }
        public string codComp { get; set; }
        public string numeroSerie { get; set; }
        public string numero { get; set; }
        public string fechaEmision { get; set; }
        public string monto { get; set; }
    }
}
