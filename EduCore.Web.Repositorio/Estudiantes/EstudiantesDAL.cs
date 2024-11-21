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
    public class EstudiantesDAL : IEstudiantesDAL
    {
        private readonly string connectionString;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public EstudiantesDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<Estudiantes> Consultar(Estudiantes obj)
        {
            try
            {
                List<Estudiantes> res;
                using DapperManager<Estudiantes> dapper = new SqlConnectionFactory<Estudiantes>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_ESTUDIANTES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.ESTUDIANTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<Estudiantes> { new() { CC = "0", NombreCompleto = msg + ex.Message } };
            }

        }

        public object Insertar(EstudiantesDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    parameters.Add("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
                    parameters.Add("strNombreCompleto", string.IsNullOrEmpty(obj.NombreCompleto) ? null : obj.NombreCompleto);
                    parameters.Add("strDireccion", string.IsNullOrEmpty(obj.Direccion) ? null : obj.Direccion);
                    parameters.Add("strTelefono", string.IsNullOrEmpty(obj.Telefono) ? null : obj.Telefono);
                    parameters.Add("strCorreo", string.IsNullOrEmpty(obj.Correo) ? null : obj.Correo);
                    parameters.Add("dtFechaNacimiento", obj.FechaNacimiento == default ? null : obj.FechaNacimiento);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_ESTUDIANTES, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.ESTUDIANTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };

            }
        }

        public object Actualizar(EstudiantesDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                    parameters.Add("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
                    parameters.Add("strNombreCompleto", string.IsNullOrEmpty(obj.NombreCompleto) ? null : obj.NombreCompleto);
                    parameters.Add("strDireccion", string.IsNullOrEmpty(obj.Direccion) ? null : obj.Direccion);
                    parameters.Add("strTelefono", string.IsNullOrEmpty(obj.Telefono) ? null : obj.Telefono);
                    parameters.Add("strCorreo", string.IsNullOrEmpty(obj.Correo) ? null : obj.Correo);
                    parameters.Add("dtFechaNacimiento", string.IsNullOrEmpty(Convert.ToString(obj.FechaNacimiento)) ? null : obj.FechaNacimiento);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_ESTUDIANTES, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.ESTUDIANTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Eliminar(Estudiantes obj)
        {
            try
            {
                int res;
                using (DapperManager<Estudiantes> Dapper = new SqlConnectionFactory<Estudiantes>(connectionString).GetConnectionManager())
                {

                    Dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    Dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
                    res = Dapper.Execute(ProcedimientosAlmacenados.CRUD_ESTUDIANTES);
                }
                return new { filas = res, exitoso = true, error = string.Empty };

            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.ESTUDIANTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }
    }
}