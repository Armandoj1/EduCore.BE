using EduCore.Web.Transversales.Base;
using EduCore.Web.Transversales.Constantes;
using log4net;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace EduCore.Web.Transversales
{
    public class Config : IConfig
    {
        private IConfigurationRoot root;
        private ConfigurationBuilder configBuild;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        public Config()
        {
            configBuild = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuild.AddJsonFile(path, false);
            root = configBuild.Build();
        }
        public string GetConnectionString(string connectionName)
        {
            try
            {
                return root.GetConnectionString(connectionName) ?? string.Empty;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int line = frame.GetFileLineNumber();

                LogicalThreadContext.Properties["Line"] = line;
                log.Error($"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message, ex);
                return $"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message;
            }
        }

        public string ObtenerValor(string ruta)
        {
            try
            {
                return root[ruta] ?? string.Empty;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int line = frame.GetFileLineNumber();

                LogicalThreadContext.Properties["Line"] = line;
                log.Error($"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message, ex);
                return $"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message;
            }
        }

        public List<int> ObtenerLista(string seccion)
        {
            try
            {
                return root.GetSection(seccion).GetChildren().ToList().Select(x => Convert.ToInt32(x.Value)).ToList() ?? new List<int>();
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int line = frame.GetFileLineNumber();

                LogicalThreadContext.Properties["Line"] = line;
                log.Error($"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message, ex);
                return new List<int>();
            }
        }

        public List<string> ObtenerListaString(string seccion)
        {
            try
            {
                return root.GetSection(seccion).GetChildren().ToList().Select(x => x.Value!.ToString()).ToList() ?? new List<string>();
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int line = frame.GetFileLineNumber();

                LogicalThreadContext.Properties["Line"] = line;
                log.Error($"{Mensajes.ERROR_CADENA_CONEXION} : " + ex.Message, ex);
                return new List<string>();
            }
        }

        public class AzureAccountInfo
        {
            public string carpetaProcesados { get; set; }

            public string carpetaVariables { get; set; }

            public string connection { get; set; }

            public string accountName { get; set; }

            public string accountKey { get; set; }

            public string containername { get; set; }

            public string error { get; set; }

            public string queuename { get; set; }

            public int intHorasEnCola { get; set; }

            public string rutaImagenesEncuestas { get; set; }

            public string rutaCarpetaContingencia { get; set; }

            public string carpetaArchivosMaestros { get; set; }

            public string carpetaMoverArchivoMaestros { get; set; }

            public string CarpetaMoverArchMaestrosParciales { get; set; }

            public string CarpetaMoverArchMaestrosFallidos { get; set; }

            public int pesoMaximoArchivo { get; set; }

            public bool EsConnexionQueueSAS { get; set; }

            public string TokenSAS { get; set; }

            public string UrlQueueSAS { get; set; }

            public string CarpetaMoverArchivosFallidos { get; set; }
        }

        public static class Dinamico
        {
            public static object GetPropValue(object src, string propName)
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public static string GetCurrentMethod()
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                return sf.GetMethod().Name;
            }
        }
    }
}