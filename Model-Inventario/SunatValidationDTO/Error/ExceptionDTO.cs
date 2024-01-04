using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.InventarioDTO.Error
{
    public class ExceptionDTO : Exception
    {
        public ErrorDetail error { get; set; }
    }
    public class ErrorDetail
    {
        public string DetailError { get; set; }
        public string Message { get; set; }
        public string CodError { get; set; }
        public int StatusCode { get; set; }
    }
}
