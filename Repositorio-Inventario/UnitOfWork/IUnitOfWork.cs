using Repositorio_Inventario.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();

        public ITDocumentosAValidarRepository TDocumentosAValidarRepository { get; }
        public ISunatDataRepository SunatDataRepository { get; }
        public IMenuGreRepository MenuGreRepository { get; }
    }
}
