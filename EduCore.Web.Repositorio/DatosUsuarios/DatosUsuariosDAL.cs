using Dapper;
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
    public class DatosUsuariosDAL : IDatosUsuariosDAL
    {
        private readonly string connectionString;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DatosUsuariosDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public object ActualizarEstudiante(DatosUsuarios obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", 1);
                    paramaters.Add("strID_CC", obj.CC);
                    paramaters.Add("strNombreCompleto", obj.NombreCompleto);
                    paramaters.Add("strFechaNacimiento", obj.FechaNacimiento);
                    paramaters.Add("strDireccion", obj.Direccion);
                    paramaters.Add("strTelefono", obj.Telefono);
                    paramaters.Add("strCorreo", obj.Correo);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DATOS_USUARIOS, paramaters, commandType: CommandType.StoredProcedure);
                    return result;

                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.ESTUDIANTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return false;
            }
        }
   
        public object ActualizarDocente(DatosUsuarios obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", 2);
                    paramaters.Add("strID_CC", obj.CC);
                    paramaters.Add("strNombreCompleto", obj.NombreCompleto);
                    paramaters.Add("strFechaNacimiento", obj.FechaNacimiento);
                    paramaters.Add("strDireccion", obj.Direccion);
                    paramaters.Add("strTelefono", obj.Telefono);
                    paramaters.Add("strCorreo", obj.Correo);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DATOS_USUARIOS, paramaters, commandType: CommandType.StoredProcedure);
                    return result;                    

                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return false;
            }
        }

        public object ActualizarDirectivo(DatosUsuarios obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", 3);
                    paramaters.Add("strID_CC", obj.CC);
                    paramaters.Add("strNombreCompleto", obj.NombreCompleto);
                    paramaters.Add("strFechaNacimiento", obj.FechaNacimiento);
                    paramaters.Add("strDireccion", obj.Direccion);
                    paramaters.Add("strTelefono", obj.Telefono);
                    paramaters.Add("strCorreo", obj.Correo);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DATOS_USUARIOS, paramaters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DATOS_USUARIOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return false;
            }
        }

        public object ActualizarContrasena(ActualizarContrasena obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", 4);
                    paramaters.Add("strID_CC", obj.CC);
                    paramaters.Add("strContrasenaActual", obj.ContrasenaActual);
                    paramaters.Add("strContrasena", obj.Contrasena);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DATOS_USUARIOS, paramaters, commandType: CommandType.StoredProcedure);
                    if (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302)
                    {
                        return new { filas = 0, exitoso = false, error = result.responseMessage };
                    }
                    int filas = result?.filas ?? 0;
                    return new { filas = filas, exitoso = true, error = string.Empty };
                }

            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DATOS_USUARIOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return false;
            }
        }
    }
}