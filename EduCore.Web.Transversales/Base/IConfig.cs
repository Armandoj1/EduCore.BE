namespace EduCore.Web.Transversales.Base
{
    public interface IConfig
    {
        string GetConnectionString(string connectionName);
    }
}

