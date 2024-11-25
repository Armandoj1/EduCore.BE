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
    public class EventosDAL : IEventosDAL
    {
        private readonly string _connectionString;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public EventosDAL()
        {
            var objConfig = new Config();
            _connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<Eventos> Consultar(Eventos eventos)
        {
            try
            {
                List<Eventos> res;
                using DapperManager<Eventos> dapper = new SqlConnectionFactory<Eventos>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("UsuarioCreadorID", eventos.UsuarioCreadorID);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_EVENTOS).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<Eventos> { new() { Titulo = string.Empty, Descripcion = msg + ex.Message } };
            }
        }
        
        public List<ListadoUtilidades> ConsultarEstudiantes(ListadoUtilidades listadoUtilidades)
        {
            try
            {

                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 6);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { EstudianteCC = string.Empty, NombreCompleto = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> ConsultarTiposEventos(ListadoUtilidades listadoUtilidades)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 7);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { TipoEventoID = 0, Nombre = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades listadoUtilidades)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 3);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { GradoID = 0, NombreGrado = msg + ex.Message } };
            }
        }

        public object Insertar(EventosDTO eventos)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@intOpcion", (int)EnumTipoProceso.Insertar);
                    parameters.Add("@Titulo", eventos.Titulo);
                    parameters.Add("@Descripcion", eventos.Descripcion);
                    parameters.Add("@FechaInicio", eventos.FechaInicio);
                    parameters.Add("@FechaFin", eventos.FechaFin);
                    parameters.Add("@TipoEvento", eventos.TipoEventoID);
                    parameters.Add("@UsuarioCreadorID", eventos.UsuarioCreadorID);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_EVENTOS, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Actualizar(EventosUpdateDTO eventos)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@intOpcion", (int)EnumTipoProceso.Actualizar);
                    parameters.Add("EventoID", eventos.EventoID);
                    parameters.Add("@Titulo", eventos.Titulo);
                    parameters.Add("@Descripcion", eventos.Descripcion);
                    parameters.Add("@FechaInicio", eventos.FechaInicio);
                    parameters.Add("@FechaFin", eventos.FechaFin);
                    parameters.Add("TipoEvento", eventos.TipoEventoID);
                    parameters.Add("GradoIDs", eventos.GradoID);
                    parameters.Add("@UsuarioCreadorID", eventos.UsuarioCreadorID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_EVENTOS, parameters, commandType: CommandType.StoredProcedure);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Eliminar(Eventos eventos)
        {
            try
            {
                int res;
                using (DapperManager<Eventos> dapper = new SqlConnectionFactory<Eventos>(_connectionString).GetConnectionManager())
                {
                    dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    dapper.AddParameter("EventoID", eventos.EventoID);

                    res = dapper.Execute(ProcedimientosAlmacenados.CRUD_EVENTOS);
                }

                return new { filas = res, exitoso = true, error = string.Empty };
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.EVENTOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }
    }
}