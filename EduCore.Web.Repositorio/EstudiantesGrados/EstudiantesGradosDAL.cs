using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Entidades;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace EduCore.Web.Repositorio 
{
    public class EstudiantesGradosDAL : IEstudiantesGradosDAL
    {
        public readonly string connectionString;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public EstudiantesGradosDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<EstudiantesGrados> Consultar(EstudiantesGrados obj)
        {
            try
            {
                List<EstudiantesGrados> res;
                using DapperManager<EstudiantesGrados> dapper = new SqlConnectionFactory<EstudiantesGrados>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.EstudianteCC) ? null : obj.EstudianteCC);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_ESTUDIANTES_GRADOS).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.ESTUDIANTES_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<EstudiantesGrados> { new() { EstudianteCC = "0", NombreCompleto = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 3);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { GradoID = 0, NombreGrado = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> ConsultarEstudiantes(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 5);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { GradoID = 0, NombreGrado = msg + ex.Message } };
            }
        }

        public object Insertar(EstudiantesGradosDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    parameters.Add("strCC", string.IsNullOrEmpty(obj.EstudianteCC) ? null : obj.EstudianteCC);
                    parameters.Add("intGradoID", obj.GradoID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_ESTUDIANTES_GRADOS, parameters, commandType: CommandType.StoredProcedure);

                    if (result != null && (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302))
                    {
                        return new { filas = 0, exitoso = false, error = result.responseMessage };
                    }

                    int filas = result?.filas ?? 0;
                    return new { filas = filas, exitoso = true, error = string.Empty };

                } 
            }
            catch (Exception ex)
            {

                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.ESTUDIANTES_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };

            }
        }

        public object Actualizar(EstudiantesGradosDTO obj)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                    parameters.Add("strCC", string.IsNullOrEmpty(obj.EstudianteCC) ? null : obj.EstudianteCC);
                    parameters.Add("intGradoID", obj.GradoID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_ESTUDIANTES_GRADOS, parameters, commandType: CommandType.StoredProcedure);

                    if(result != null && (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302))
                    {
                        return new { filas = 0, exitoso = false, error = result.responseMessage };
                    }

                    int filas = result?.fillas ?? 0;
                    return new {filas = filas, exitoso = true,  error = string.Empty};
                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.ESTUDIANTES_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Eliminar(EstudiantesGrados obj)
        {
            try
            {
                int res;
                using (DapperManager<EstudiantesGrados> Dapper = new SqlConnectionFactory<EstudiantesGrados>(connectionString).GetConnectionManager())
                {
                    Dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    Dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.EstudianteCC) ? null : obj.EstudianteCC);
                    Dapper.AddParameter("intGradoID", obj.GradoID); 
                    res = Dapper.Execute(ProcedimientosAlmacenados.CRUD_ESTUDIANTES_GRADOS);
                }
                return new { filas = res, exitoso = true, error = string.Empty };
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.ESTUDIANTES_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }
    }
}