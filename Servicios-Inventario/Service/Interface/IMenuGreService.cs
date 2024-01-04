using Model_Inventario.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_Inventario.Service.Interface
{
    public interface IMenuGreService
    {
        List<T_MenuGreDTO> ObtenerMenuPorEmpresa(string ruc, string db);
    }
}
