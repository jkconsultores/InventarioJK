using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Dapper.Interface
{
    public interface IDapperRepository
    {
        string QueryDapper(string sql, object parametros);
        string QueryDapper(string sql, object parametros, int timeoutMinutos);
        string FirstOrDefault(string sql, object parametros);
        string FirstOrDefault(string sql, object parametros, int timeoutMinutos);

        string QuerySPDapper(string sql, object parametros);
        string QuerySPDapper(string sql, object parametros, int timeoutMinutos);
        string QuerySPFirstOrDefault(string sql, object parametros);
        string QuerySPFirstOrDefault(string sql, object parametros, int timeoutMinutos);
    }
}
