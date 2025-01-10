using Dapper;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge
{
    public class CustomRepository : ICustomRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<T>> Execute<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity
        {

            using (var multi = await _dbConnection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
            {
                var data = await multi.ReadAsync<T>();
                return data;
            }
        }
        public async Task<(IEnumerable<T>, int)> ExecuteStoredProcedure<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity
        {

            using (var multi = await _dbConnection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
            {
                var data = await multi.ReadAsync<T>();
                int count = multi.Read<int>().SingleOrDefault();
                return (data, count);
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureMany<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity
        {

            using (var multi = await _dbConnection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
            {
                var result = await multi.ReadAsync<T>();
                return result;
            }
        }

        public async Task<T> ExecuteStoredProcedureSingle<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity
        {

            using (var single = await _dbConnection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
            {
                var data = await single.ReadSingleAsync<T>();
                return data;
            }
        }

        public async Task<IEnumerable<T>> ExecuteBySqlQuery<T>(string query,object? parameters=null, int? commandTimeout = null) where T : class , IEntity
        {
            return await _dbConnection.QueryAsync<T>(query, parameters, commandTimeout: commandTimeout).ConfigureAwait(false);
        }

        public async Task<int> ExecuteStoredProcedureSingleWithStatus<T>(string storedProcedure, object parameters = null)
        {

            using (var single = await _dbConnection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                var status = await single.ReadSingleAsync<int>();
                return status;
            }
        }
        
        public async Task<int> ExecuteNonQuery(string query, object? parameters = null, CommandType commandType = CommandType.StoredProcedure, int? commandTimeout = null)
        {
            return await _dbConnection.ExecuteAsync(query, parameters, commandType: commandType, commandTimeout: commandTimeout).ConfigureAwait(false);
        }

    }
}
