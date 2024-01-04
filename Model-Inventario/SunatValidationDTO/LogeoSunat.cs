using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.SunatValidationDTO
{
    public class LogeoSunat
    {
        public string grant_type { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }

        public LogeoSunat(string clientId, string clientSecret)
        {
            grant_type = "client_credentials";
            scope = "https://api.sunat.gob.pe/v1/contribuyente/contribuyentes";
            client_id = clientId;
            client_secret = clientSecret;
        }
    }
    public class responseLOGin
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

}
