using Model_Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_Inventario.Service.Interface
{
    public interface ISunatDataService
    {
        bool AddCredentials(SunatData data, string bD_Sql);
    }
}
