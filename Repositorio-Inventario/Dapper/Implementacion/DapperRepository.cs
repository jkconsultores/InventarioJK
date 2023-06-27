using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositorio_Inventario.Dapper.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario.Dapper.Implementacion
{
    public class DapperRepository : IDapperRepository
    {
        private int _timeout = 20 * 60;
        private SqlDbContext _context;

        public DapperRepository()
        {
            _context = new SqlDbContext();
        }

        public DapperRepository(SqlDbContext context)
        {
            _context = context;
        }

        public string QueryDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.Text, commandTimeout: _timeout).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QueryDapper(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.Text, commandTimeout: timeout).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string FirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, param: parametros,
                commandType: CommandType.Text, commandTimeout: _timeout).FirstOrDefault();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string FirstOrDefault(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, param: parametros,
                commandType: CommandType.Text, commandTimeout: timeout).FirstOrDefault();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).ToList();
            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }
        public string QuerySPDapper(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).ToList();
            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPFirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).FirstOrDefault();
            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }
        public string QuerySPFirstOrDefault(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).FirstOrDefault();
            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }


        private int ConvertirTimeOutSegundos(int timeoutMinutos)
        {
            return timeoutMinutos * 60;
        }
    }
}
