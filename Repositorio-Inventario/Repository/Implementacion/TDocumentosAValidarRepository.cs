using Microsoft.IdentityModel.Tokens;
using Model_Inventario.DTO;
using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Model_Inventario.SunatValidationDTO;
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
    public class TDocumentosAValidarRepository : GenericRepository<DocumentosAValidar>, ITDocumentosAValidarRepository
    {
        private SqlDbContext dbContext;
        private DapperRepository _dapperRepositoryPortal;

        public TDocumentosAValidarRepository(SqlDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            _dapperRepositoryPortal = new DapperRepository(dbContext);
        }
        public List<DocumentosAValidar> ObtenerTodasLasValidacionesPendientes(string db)
        {
            try
            {
                dbContext = new SqlDbContext(db);
                return dbContext.T_DocumentosAValidar.Where(x=>x.Procesado==false).ToList();
            }catch(Exception ex) { throw ex; }
        }
        public pagination obtenerReporte(DesdeHastaDTO data, string db)
        {
            try
            {
                dbContext = new SqlDbContext(db);
                pagination d = new pagination();
                _dapperRepositoryPortal = new DapperRepository(dbContext);
                string sql = @"SELECT  t.tipoDocumentoRemision
                              ,t.numeroDocumentoRemision
                              ,t.serieNumero
                              ,t.tipoDocumento
                              ,t.FechaEmision
                              ,t.MontoTotal
                              ,t.Procesado
                              ,t.FechaCreacion
                              ,u.NombreUsuario
                              ,t.FechaDeConsulta
                              ,t.EstadoCp
                              ,t.estadoRuc
                              ,t.condDomiRuc
                              ,t.Mensaje
                              ,t.EstadoDoc
                              ,t.Observaciones
                          FROM dbo.T_DocumentosAValidar as t inner join USUARIO as u 
                          on t.IdUsuarioCreacion = u.USUARIOID 
                          where t.FechaEmision >= @desde 
                                AND t.FechaEmision <= @hasta ";
                if (!data.estadoCp.IsNullOrEmpty())
                {
                    sql = sql + $" AND t.EstadoCp = '{data.estadoCp}'";
                }
                if (!data.serieNumero.IsNullOrEmpty())
                {
                    sql = sql + $" AND t.serieNumero  like '%{data.serieNumero}%'";
                }
                sql = sql + " order by fechaemision desc offset @inicio rows fetch next @cantidad rows only ";

                var respuesta = _dapperRepositoryPortal.QueryDapper(sql, new { desde = data.Desde, hasta = data.Hasta, inicio = data.inicio, cantidad = data.cantidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    d.Datos = JsonConvert.DeserializeObject<List<DocumentosAValidarDTO>>(respuesta);
                    var ssl = @"SELECT COUNT(*) as TotalRegistros  FROM dbo.T_DocumentosAValidar as t inner join USUARIO as u 
                          on t.IdUsuarioCreacion = u.USUARIOID where t.FechaEmision >= @desde AND t.FechaEmision <= @hasta";
                    var rr = _dapperRepositoryPortal.QueryDapper(sql, new
                    {
                        desde = data.Desde,
                        hasta = data.Hasta,
                    });
                    d.TotalRegistros = JsonConvert.DeserializeObject<CantidadRegistrosDTO>(rr).TotalRegistros;
                    
                }
                return d;

            }
            catch (Exception ex) { throw ex; }
        }
        public void agregarData(DocumentosAValidar data, string db)
        {
            try
            {
                dbContext = new SqlDbContext(db);
                dbContext.T_DocumentosAValidar.Add(data);
                dbContext.SaveChanges();
            }
            catch (Exception ex) { throw ex; }
        }
        public void aActualziarData(DocumentosAValidar data, string db)
        {
            try
            {
                dbContext = new SqlDbContext(db);
                var resp = dbContext.T_DocumentosAValidar.FirstOrDefault(x => x.numeroDocumentoRemision == data.numeroDocumentoRemision && x.serieNumero == data.serieNumero && x.tipoDocumento == data.tipoDocumento && x.tipoDocumentoRemision == data.tipoDocumentoRemision);
                if (resp != null)
                {
                    resp.EstadoCp = data.EstadoCp;
                    resp.EstadoDoc = data.EstadoDoc;
                    resp.estadoRuc = data.estadoRuc;
                    resp.condDomiRuc = data.condDomiRuc;
                    resp.Observaciones = data.Observaciones;
                    resp.Mensaje = data.Mensaje;
                    resp.FechaDeConsulta = data.FechaDeConsulta;
                    resp.Procesado = true;
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
