using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.QTKD.DL.Connection
{
    /// <summary>
    /// Kết nối đến database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Connection<T>
    {
        private MySqlConnection _connect;

        /// <summary>
        /// kết nối db
        /// </summary>
        public Connection()
        {
            _connect = new MySqlConnection(DataContext.MySqlConnectionString);
        }

        public int ExecuteProc(string procedureName, DynamicParameters? parameters)
        {
            return _connect.Execute(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public GridReader QueryMultipleProc(string procedureName, DynamicParameters? parameters)
        {
            return _connect.QueryMultiple(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<T> QueryProc(string procedureName, DynamicParameters? parameters)
        {
            return _connect.Query<T>(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public string QueryFirstOrDefaultProc(string procedureName)
        {
            return _connect.QueryFirstOrDefault<string>(procedureName, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Đóng kết nối
        /// </summary>
        public void CloseConnect()
        {
            _connect.Close();
        }
    }
}
