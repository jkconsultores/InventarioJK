using Model_Inventario.DTO;
using Repositorio_Inventario.UnitOfWork;
using Servicios_Inventario.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_Inventario.Service.Implementacion
{
    public class MenuGreService : IMenuGreService
    {
        private readonly IUnitOfWork unitOfWork;

        public MenuGreService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<T_MenuGreDTO> ObtenerMenuPorEmpresa(string ruc, string db)
        {
            try
            {
                return unitOfWork.MenuGreRepository.ObtenerMenuDeEmpresa(ruc, db);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
