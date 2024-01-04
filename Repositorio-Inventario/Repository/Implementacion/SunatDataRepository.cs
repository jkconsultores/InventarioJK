using Model_Inventario.Entidades;
using Repositorio_Inventario.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Implementacion
{
    public class SunatDataRepository : GenericRepository<SunatData>, ISunatDataRepository
    {
        private SqlDbContext dbContext;

        public SunatDataRepository(SqlDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddCredentials(SunatData data, string bD_Sql)
        {
            try
            {
                dbContext = new SqlDbContext(bD_Sql);
                dbContext.SunatData.Add(data);
                dbContext.SaveChanges();
                return true;
            }catch(Exception ex) { return false; }
        }
    }
}
