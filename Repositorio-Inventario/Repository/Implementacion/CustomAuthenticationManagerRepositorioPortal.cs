using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Newtonsoft.Json;
using Repositorio_Inventario.Dapper.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Repository.Implementacion
{
    public class CustomAuthenticationManagerRepositorioPortal
    {
        private DapperRepository _dapperRepositoryPortal;
        public CustomAuthenticationManagerRepositorioPortal()
        {
            _dapperRepositoryPortal = new DapperRepository();
        }
        public Empresa ObtenerEmpresa(UsuarioLoginDTO usuarioLog, SqlDbContext contexto)
        {
            try
            {
                _dapperRepositoryPortal = new DapperRepository(contexto);
                var _query = string.Empty;
                Empresa registro = new Empresa();
                _query = @"SELECT  *
                            FROM EMPRESA
                            WHERE DESCRIPCION=@empresa ";
                var respuesta = _dapperRepositoryPortal.FirstOrDefault(_query, new { usuarioLog.empresa });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    registro = JsonConvert.DeserializeObject<Empresa>(respuesta);
                }
                else
                {
                    registro = null;
                }

                return registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuariosReturnPortalDTO AutenticacionUsuarioPortal(UsuarioLoginDTO usuarioLog, Empresa emp)
        {
            try
            {
                SqlDbContext sqlconte = new SqlDbContext(emp.cadenaconexion);
                _dapperRepositoryPortal = new DapperRepository(sqlconte);

                var _query = string.Empty;
                UsuariosReturnPortalDTO registro = new UsuariosReturnPortalDTO();
                _query = @"SELECT  
                            USUARIOID AS Id,
                            [NOMBREUSUARIO] AS Usuario,
                            Rol as Rol,
                            CORREOELECTRONICO as correo
                            FROM dbo.Usuario as us
                            WHERE us.contrasena = @contrasena AND us.nombreusuario = @nombreusuario";
                var respuesta = _dapperRepositoryPortal.FirstOrDefault(_query, new { usuarioLog.nombreusuario, usuarioLog.contrasena });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    registro = JsonConvert.DeserializeObject<UsuariosReturnPortalDTO>(respuesta);
                }
                else
                {
                    registro = null;
                }

                return registro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public SunatData ObtenerCredencialesSuant(int ruc,string conexion)
        {
            try
            {
                SqlDbContext sqlconte = new SqlDbContext(conexion);
                _dapperRepositoryPortal = new DapperRepository(sqlconte);

                var _query = string.Empty;
                SunatData registro = new SunatData();
                _query = @"SELECT  
                            *
                            FROM SunatData as us
                            WHERE us.RucEmsior = @ruc";
                var respuesta = _dapperRepositoryPortal.FirstOrDefault(_query, new { ruc });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    registro = JsonConvert.DeserializeObject<SunatData>(respuesta);
                }
                else
                {
                    registro = null;
                }

                return registro;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }
}
