using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.SunatValidationDTO
{
    public class ValidationResponseSuant
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Data? data { get; set; }
    }
    public class dataObtencion
    {
        public ValidationResponseSuant? data { get; set; }
        public int statusCode { get; set; }
    }
    public class Data
    {
        public string estadoCp { get; set; }
        public string estadoRuc { get; set; }
        public string condDomiRuc { get; set; }
        public List<string> observaciones { get; set; }
    }
}

