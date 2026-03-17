using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AttendanceAPI.Data
{
    public class DataContextDapper
    {
        private readonly IConfiguration config;
        public DataContextDapper(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T>(string sql)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.QueryAsync<T>(sql);
        }

        public async Task<T?> LoadDataSingle<T>(string sql, object? parameters = null)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<bool> Executesql(string sql, object? parameters = null)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.ExecuteAsync(sql, parameters) > 0;
        }

        public async Task<int> ExecuteSqlScalar(string sql, object? parameters = null)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.ExecuteScalarAsync<int>(sql, parameters);
        }
    }
}