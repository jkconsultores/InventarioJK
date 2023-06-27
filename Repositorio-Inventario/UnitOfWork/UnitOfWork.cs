using Repositorio_Inventario.Repository.Implementacion;
using Repositorio_Inventario.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _dbContext;

        public UnitOfWork(SqlDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Commit() => _dbContext.SaveChanges();
        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
        public void Rollback() => _dbContext.Dispose();
        public async Task RollbackAsync() => await _dbContext.DisposeAsync();

        //implementacion de repositorios
        private EmpresaRepository _EmpresaRepository;
        public IEmpresaRepository EmpresaRepository
        {
            get
            {
                return _EmpresaRepository = _EmpresaRepository ?? new EmpresaRepository(_dbContext);
            }
        }
    }
}
