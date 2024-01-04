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
    public class SunatDataService : ISunatDataService
    {
        private readonly IUnitOfWork unitOfWork;

        public SunatDataService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddCredentials(SunatData data, string bD_Sql)
        {
            try
            {
                return  unitOfWork.SunatDataRepository.AddCredentials(data, bD_Sql);
            }catch { return false; }
        }
    }
}
