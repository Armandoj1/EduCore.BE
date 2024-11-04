using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Data
{
    public class SqlConnectionFactory<T> : IConnectionFactory<T>
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;
        public DapperManager<T> GetConnectionManager() => new DapperSqlServer<T>(_connectionString);
    }
}
