using Model_Inventario.DTO;
using Model_Inventario.Entidades;
using Newtonsoft.Json;
using Repositorio_Inventario.Dapper.Implementacion;
using Repositorio_Inventario.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Implementacion
{
    public class MenuGreRepository : GenericRepository<T_MenuGre>, IMenuGreRepository
    {
        private SqlDbContext dbContext;
        private DapperRepository _dapperRepositoryPortal;

        public MenuGreRepository(SqlDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<T_MenuGreDTO> ObtenerMenuDeEmpresa(string ruc, string db)
        {
            try
            {
                List<T_MenuGreDTO> menuslist = new List<T_MenuGreDTO>();
                dbContext = new SqlDbContext(db);
                _dapperRepositoryPortal = new DapperRepository(dbContext);
                var query = @"SP_Obtencion_menu_usuario";
                var respuesta = _dapperRepositoryPortal.QuerySPDapper(query, new { IdUsuario = ruc });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    menuslist = JsonConvert.DeserializeObject<List<T_MenuGreDTO>>(respuesta);
                    return menuslist.OrderBy(x => x.orden).ToList();
                }
                return menuslist;
                //var datos = dbContext.T_MenuGre.Where(x=>x.Estado==true ).OrderBy(a=>a.orden).ToList();
                //return datos;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
