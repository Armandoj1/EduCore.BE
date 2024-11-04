using Dapper;
using static Dapper.SqlMapper;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using DapperParameters;

namespace EduCore.Web.Data;

internal class DapperSqlServer<T> : DapperManager<T>
{
    public DapperSqlServer(string connectionString) => Connection = new SqlConnection(connectionString);
    public override void Openconection() => OpenConnection();
    public override void AddOutputParameter(string pName, object pValue)
    {
        Parameters ??= new DynamicParameters();
        ((DynamicParameters)Parameters).Add(string.Format(CultureInfo.CurrentCulture, "@{0}", pName), pValue, direction: ParameterDirection.Output);
    }
    public override void AddParameter(string pName, object pValue)
    {
        Parameters ??= new DynamicParameters();
        ((DynamicParameters)Parameters).Add(string.Format(CultureInfo.CurrentCulture, "@{0}", pName), pValue, direction: ParameterDirection.Input);
    }
    public override void AddParameterTable<TEntity>(string pName, string nameType, ICollection<TEntity> pValue)
    {
        Parameters ??= new DynamicParameters();
        ((DynamicParameters)Parameters).AddTable(string.Format(CultureInfo.CurrentCulture, "@{0}", pName), nameType, pValue);
    }
    public override DbTransaction CreateTransaction() => Connection.BeginTransaction();
    public override int Execute(string pStoredProcedure)
    {
        OpenConnection();
        var rowsAffected = Connection.Execute(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        Dispose();
        return rowsAffected;
    }
    public override async Task<int> ExecuteAsync(string pStoredProcedure)
    {
        OpenConnectionAsync();
        var rowsAffected = await Connection.ExecuteAsync(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        DisposeAsync();
        return rowsAffected;
    }
    public override int ExecuteInTransaction(string pStoredProcedure, DbTransaction trTransaccion)
    {
        var rowsAffected = Connection.Execute(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        Parameters = null;
        return rowsAffected;
    }
    public override async Task<int> ExecuteInTransactionAsync(string pStoredProcedure, DbTransaction trTransaccion)
    {
        var RowsAffected = await Connection.ExecuteAsync(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        Parameters = null;
        return RowsAffected;
    }
    public override Collection<T> ExecuteInTransactionGetList(string pStoredProcedure, DbTransaction trTransaccion)
    {
        var response = new ObservableCollection<T>(Connection.Query<T>(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure).ToList());
        Parameters = null;
        return response;
    }
    public override async Task<List<T>> ExecuteInTransactionGetListAsync(string pStoredProcedure, DbTransaction trTransaccion)
    {
        var response = await Connection.QueryAsync<T>(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        Parameters = null;
        return response.ToList();
    }
    public override Collection<T> GetList(string pStoredProcedure)
    {
        OpenConnection();
        Parameters ??= new DynamicParameters();

        var response = new ObservableCollection<T>(Connection.Query<T>(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure).ToList());
        Dispose();
        return response;
    }
    public override async Task<List<T>> GetListAsync(string pStoredProcedure)
    {
        OpenConnectionAsync();
        Parameters ??= new DynamicParameters();

        var response = await Connection.QueryAsync<T>(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        DisposeAsync();
        return response.ToList();
    }
    public override T GetQueryFirst(string pStoredProcedure)
    {
        OpenConnection();
        var QueryResponse = Connection.Query<T>(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        Dispose();
        return QueryResponse;
    }
    public override async Task<T> GetQueryFirstAsync(string pStoredProcedure)
    {
        OpenConnectionAsync();
        var QueryResponse = await Connection.QueryAsync<T>(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        DisposeAsync();
        return QueryResponse.FirstOrDefault();
    }
    public override T GetQueryFirstInTransaction(string pStoredProcedure, DbTransaction trTransaccion)
    {
        OpenConnection();
        var QueryResponse = Connection.Query<T>(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure).FirstOrDefault();
        Parameters = null;
        return QueryResponse;
    }
    public override async Task<T> GetQueryFirstInTransactionAsync(string pStoredProcedure, DbTransaction trTransaccion)
    {
        OpenConnectionAsync();
        var QueryResponse = await Connection.QueryAsync<T>(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        Parameters = null;
        return QueryResponse.FirstOrDefault();
    }
    public override int QueryInsert(string pStoredProcedure, string outputParameterName)
    {
        OpenConnection();
        int outputValue = Connection.ExecuteScalar<int>(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        Dispose();
        return outputValue;
    }
    public override async Task<int> QueryInsertAsync(string pStoredProcedure, string outputParameterName)
    {
        int outputValue = 0;
        OpenConnection();
        await Connection.ExecuteAsync(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        if (outputParameterName != null)
            outputValue = ((DynamicParameters)Parameters).Get<int>(outputParameterName);
        DisposeAsync();
        return outputValue;
    }
    public override int QueryInsertInTransaction(string pStoredProcedure, string outputParameterName, DbTransaction trTransaccion)
    {
        int outputValue = 0;
        Connection.Execute(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        if (outputParameterName != null)
            outputValue = ((DynamicParameters)Parameters).Get<int>(outputParameterName);
        Parameters = null;
        return outputValue;
    }
    public override async Task<int> QueryInsertInTransactionTaskAsync(string pStoredProcedure, string outputParameterName, DbTransaction trTransaccion)
    {
        int outputValue = 0;
        await Connection.ExecuteAsync(pStoredProcedure, Parameters, trTransaccion, commandType: CommandType.StoredProcedure);
        if (outputParameterName != null)
            outputValue = ((DynamicParameters)Parameters).Get<int>(outputParameterName);
        Parameters = null;
        return outputValue;
    }

    public override async Task<GridReader> QueryMultipleAsync(string pStoredProcedure)
    {
        OpenConnection();
        Parameters ??= new DynamicParameters();
        var GridReader = await Connection.QueryMultipleAsync(pStoredProcedure, Parameters, commandType: CommandType.StoredProcedure);
        return GridReader;
    }
}