namespace EduCore.Web.Data
{
    public interface IConnectionFactory<T>
    {
        DapperManager<T> GetConnectionManager();
    }
}
