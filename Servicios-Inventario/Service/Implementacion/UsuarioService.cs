using Repositorio_Inventario.UnitOfWork;
using Servicios_Inventario.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_Inventario.Service.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
