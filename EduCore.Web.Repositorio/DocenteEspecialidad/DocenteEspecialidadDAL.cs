using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Base;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using EduCore.Web.Transversales;

namespace EduCore.Web.Repositorio
{
    public class DocenteEspecialidadDAL : IDocenteEspecialidadDAL
    {
        private readonly string connectionString;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DocenteEspecialidadDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<DocenteEspecialidad> Consultar(DocenteEspecialidad obj)
        {
            try
            {
                List<DocenteEspecialidad> res;
                using DapperManager<DocenteEspecialidad> dapper = new SqlConnectionFactory<DocenteEspecialidad>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.DocenteID) ? null : obj.DocenteID);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_DOCENTE_ESPECIALIDAD).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<DocenteEspecialidad> { new() { DocenteID = "0", EspecialidadID = 0 } };
            }
        }

        public object Insertar(DocenteEspecialidadDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    paramaters.Add("strCC", obj.DocenteID);
                    paramaters.Add("intEspecialidadID", obj.EspecialidadID);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTE_ESPECIALIDAD, paramaters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Actualizar(DocenteEspecialidadDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                    paramaters.Add("strCC", obj.DocenteID);
                    paramaters.Add("intEspecialidadID", obj.EspecialidadID);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTE_ESPECIALIDAD, paramaters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public List<ListadoUtilidades> CosultarEspecialidad(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 2);
                dapper.AddParameter("intEspecialidadID", obj.EspecialidadID == 0 ? null : obj.EspecialidadID);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { EspecialidadID = 0, NombreEspecialidad = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> ConsultarDocente(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 4);
                dapper.AddParameter("strCC", obj.CC);
                dapper.AddParameter("strNombreCompleto", obj.NombreCompleto);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { CC = null, NombreCompleto = msg + ex.Message } };
            }
        }
        
        public object Eliminar(DocenteEspecialidadDTO obj)
        {
            try
            {

                int res;
                using (DapperManager<DocenteEspecialidadDTO> dapper = new SqlConnectionFactory<DocenteEspecialidadDTO>(connectionString).GetConnectionManager())
                {
                    dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.DocenteID) ? null : obj.DocenteID);
                    dapper.AddParameter("intEspecialidadID", obj.EspecialidadID);

                    res = dapper.Execute(ProcedimientosAlmacenados.CRUD_DOCENTE_ESPECIALIDAD);
                }

                return new { filas = res, exitoso = true, error = string.Empty };
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }
    }
}