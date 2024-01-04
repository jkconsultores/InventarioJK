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

        private TDocumentosAValidarRepository _TDocumentosAValidarRepository;
        public ITDocumentosAValidarRepository TDocumentosAValidarRepository
        {
            get
            {
                return _TDocumentosAValidarRepository = _TDocumentosAValidarRepository ?? new TDocumentosAValidarRepository(_dbContext);
            }
        } 
        private SunatDataRepository _SunatDataRepository;
        public ISunatDataRepository SunatDataRepository
        {
            get
            {
                return _SunatDataRepository = _SunatDataRepository ?? new SunatDataRepository(_dbContext);
            }
        }
        private MenuGreRepository _MenuGreRepository;
        public IMenuGreRepository MenuGreRepository
        {
            get
            {
                return _MenuGreRepository = _MenuGreRepository ?? new MenuGreRepository(_dbContext);
            }
        }
    }
}
