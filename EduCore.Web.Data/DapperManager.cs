using Dapper;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data;
using static Dapper.SqlMapper;

namespace EduCore.Web.Data;

public abstract class DapperManager<T> : IDisposable
{
    private DbConnection _Connection;
    private IDynamicParameters _parameters;
    protected DbConnection Connection { get => _Connection; set => _Connection = value; }
    protected IDynamicParameters Parameters { get => _parameters; set => _parameters = value; }
    public abstract void Openconection();
    public abstract void AddParameter(string pName, object pValue);
    public abstract void AddParameterTable<TEntity>(string pName, string nameType, ICollection<TEntity> pValue);
    public abstract void AddOutputParameter(string pName, object pValue);
    public abstract DbTransaction CreateTransaction();
    public abstract int Execute(string pStoredProcedure);
    public abstract Task<int> ExecuteAsync(string pStoredProcedure);
    public abstract int ExecuteInTransaction(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract Task<int> ExecuteInTransactionAsync(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract Collection<T> ExecuteInTransactionGetList(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract Task<List<T>> ExecuteInTransactionGetListAsync(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract Collection<T> GetList(string pStoredProcedure);
    public abstract Task<List<T>> GetListAsync(string pStoredProcedure);
    public abstract T GetQueryFirst(string pStoredProcedure);
    public abstract Task<T> GetQueryFirstAsync(string pStoredProcedure);
    public abstract T GetQueryFirstInTransaction(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract Task<T> GetQueryFirstInTransactionAsync(string pStoredProcedure, DbTransaction trTransaccion);
    public abstract int QueryInsert(string pStoredProcedure, string outputParameterName);
    public abstract Task<int> QueryInsertAsync(string pStoredProcedure, string outputParameterName);
    public abstract int QueryInsertInTransaction(string pStoredProcedure, string outputParameterName, DbTransaction trTransaccion);
    public abstract Task<int> QueryInsertInTransactionTaskAsync(string pStoredProcedure, string outputParameterName, DbTransaction trTransaccion);
    public abstract Task<GridReader> QueryMultipleAsync(string pStoredProcedure);
    internal bool OpenConnection()
    {
        if (Connection.State != ConnectionState.Open)
            Connection.Open();
        return true;
    }
    internal bool OpenConnectionAsync()
    {
        if (Connection.State != ConnectionState.Open)
            Connection.OpenAsync();
        return true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        Parameters = new DynamicParameters();
        if (Connection.State != ConnectionState.Closed)
            Connection.Close();
    }
    public void DisposeAsync()
    {
        DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    private void DisposeAsync(bool disposing)
    {
        Parameters = new DynamicParameters();
        if (Connection.State != ConnectionState.Closed)
            Connection.CloseAsync();
    }
}
