using Model_Inventario.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Interface
{
    public interface IMenuGreRepository
    {
        List<T_MenuGreDTO> ObtenerMenuDeEmpresa(string ruc, string db);
    }
}
