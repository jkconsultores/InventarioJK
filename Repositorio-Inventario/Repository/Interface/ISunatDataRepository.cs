using Model_Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Interface
{
    public interface ISunatDataRepository
    {
        bool AddCredentials(SunatData data, string bD_Sql);
    }
}
