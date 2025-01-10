using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge
{
    public interface ICustomRepository
    {
        Task<(IEnumerable<T>, int)> ExecuteStoredProcedure<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity;
        Task<T> ExecuteStoredProcedureSingle<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity;
        Task<IEnumerable<T>> ExecuteStoredProcedureMany<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity;
        Task<IEnumerable<T>> ExecuteBySqlQuery<T>(string query, object? parameters=null, int? commandTimeout = null) where T : class, IEntity;
        Task<IEnumerable<T>> Execute<T>(string storedProcedure, object parameters = null, int? commandTimeout = null) where T : class, IEntity;
        Task<int> ExecuteNonQuery(string query, object? parameters = null, CommandType commandType = CommandType.StoredProcedure, int? commandTimeout = null);
        Task<int> ExecuteStoredProcedureSingleWithStatus<T>(string storedProcedure, object parameters = null);
    }
}
