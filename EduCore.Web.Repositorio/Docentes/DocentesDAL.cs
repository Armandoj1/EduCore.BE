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
    public class DocentesDAL : IDocentesDAL
    {
        private readonly string connectionString;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DocentesDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<Docentes> Consultar(Docentes obj)
        {
            try
            {
                List<Docentes> res;
                using DapperManager<Docentes> dapper = new SqlConnectionFactory<Docentes>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_DOCENTES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<Docentes> { new() { CC = "0", NombreCompleto = msg + ex.Message } };
            }
        }

        public List<ListadoUtilidades> Consultar(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 1);
                dapper.AddParameter("strMateriaID", string.IsNullOrEmpty(obj.MateriaID) ? null : obj.MateriaID);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { MateriaID = "0", NombreMateria = msg + ex.Message } };
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

        public List<ListadoUtilidades> ConsultarGrado(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 3);
                dapper.AddParameter("intGradoID", obj.GradoID == 0 ? null : obj.GradoID);

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

        public object Insertar(DocentesDTO obj)
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
                    parameters.Add("dtFechaNacimiento", obj.FechaNacimiento == default ? null : obj.FechaNacimiento);
                    parameters.Add("strDireccion", string.IsNullOrEmpty(obj.Direccion) ? null : obj.Direccion);
                    parameters.Add("strTelefono", string.IsNullOrEmpty(obj.Telefono) ? null : obj.Telefono);
                    parameters.Add("strCorreo", string.IsNullOrEmpty(obj.Correo) ? null : obj.Correo);


                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTES, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Actualizar(DocentesDTO obj)
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
                    parameters.Add("dtFechaNacimiento", obj.FechaNacimiento == default ? null : obj.FechaNacimiento);
                    parameters.Add("strDireccion", string.IsNullOrEmpty(obj.Direccion) ? null : obj.Direccion);
                    parameters.Add("strTelefono", string.IsNullOrEmpty(obj.Telefono) ? null : obj.Telefono);
                    parameters.Add("strCorreo", string.IsNullOrEmpty(obj.Correo) ? null : obj.Correo);


                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTES, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }

        public object Eliminar(Docentes obj)
        {
            try
            {
                int res;
                using (DapperManager<Docentes> dapper = new SqlConnectionFactory<Docentes>(connectionString).GetConnectionManager())
                {
                    dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);

                    res = dapper.Execute(ProcedimientosAlmacenados.CRUD_DOCENTES);
                }

                return new { filas = res, exitoso = true, error = string.Empty };
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
        }
    }
}