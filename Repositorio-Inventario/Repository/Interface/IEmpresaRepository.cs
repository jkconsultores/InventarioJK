using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Interface
{
    public interface IEmpresaRepository
    {
        Empresa AgregarEmrpesa(EmpresaDTO empresa);
    }
}
