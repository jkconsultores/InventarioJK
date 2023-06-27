using AutoMapper;
using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Repositorio_Inventario.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Implementacion
{
    public class EmpresaRepository : GenericRepository<Empresa>, IEmpresaRepository
    {
        private readonly SqlDbContext dbContext;
        private IMapper mapper;

        public EmpresaRepository(SqlDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            var config = new MapperConfiguration(Cfg =>
            {
                Cfg.CreateMap<Empresa, EmpresaDTO>().ReverseMap();
            });
            this.mapper = new Mapper(config);
        }

        public Empresa AgregarEmrpesa(EmpresaDTO empresa)
        {
            try
            {
                Empresa data = mapper.Map<Empresa>(empresa);
                dbContext.EMPRESA.ToList();
                return data;
            }catch(Exception ex) { throw ex; }
        }
    }
}
