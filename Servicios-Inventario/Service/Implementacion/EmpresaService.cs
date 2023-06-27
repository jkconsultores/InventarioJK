using Model_Inventario.Entidades;
using Repositorio_Inventario.UnitOfWork;
using Servicios_Inventario.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_Inventario.Service.Implementacion
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmpresaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Empresa ObtenerEmpresa()
        { //logica 
            var datos = unitOfWork.EmpresaRepository.AgregarEmrpesa(new Model_Inventario.InventarioDTO.EmpresaDTO());
            return new Empresa();
        }
    }
}
