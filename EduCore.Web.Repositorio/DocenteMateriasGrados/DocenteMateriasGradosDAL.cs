using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Base;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using log4net;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using EduCore.Web.Transversales;

namespace EduCore.Web.Repositorio
{

    public class DocenteMateriasGradosDAL : IDocenteMateriasGradosDAL
    {
        private readonly string connectionString;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);


        public DocenteMateriasGradosDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<DocenteMateriasGradosList> Consultar(DocenteMateriasGradosList obj)
        {
            try
            {
                List<DocenteMateriasGradosList> res;
                using DapperManager<DocenteMateriasGradosList> dapper = new SqlConnectionFactory<DocenteMateriasGradosList>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("strCC", string.IsNullOrEmpty(obj.DocenteID) ? null : obj.DocenteID);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_DOCENTE_MATERIAS_GRADOS).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<DocenteMateriasGradosList> { new() { DocenteID = "0", MateriaID = msg + ex.Message } };
            }
        }

        public object Insertar(DocenteMateriasGrados obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    paramaters.Add("strCC", obj.DocenteID);
                    paramaters.Add("strMateriaID", obj.MateriaID);
                    paramaters.Add("intGradoID", obj.GradoID);

                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTE_MATERIAS_GRADOS, paramaters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new {filas = 0, exitoso = false, error = msg + ex.Message };
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

        public List<ListadoUtilidades> ConsultarMaterias(ListadoUtilidades obj)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 1);
                dapper.AddParameter("strMateriaID", obj.MateriaID);
                dapper.AddParameter("strNombreMateria", obj.NombreMateria);

                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_UTILIDADES).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades> { new() { MateriaID = null, NombreMateria = msg + ex.Message } };
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

        public object Actualizar(DocenteMateriasGrados obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                    paramaters.Add("strCC", obj.DocenteID);
                    paramaters.Add("strMateriaID", obj.MateriaID);
                    paramaters.Add("intGradoID", obj.GradoID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_DOCENTE_MATERIAS_GRADOS, paramaters, commandType: CommandType.StoredProcedure);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { DocenteID = "0", MateriaID = msg + ex.Message };
            }
        }

        public object Eliminar(DocenteMateriasGrados obj)
        {
            try
            {
                using DapperManager<DocenteMateriasGrados> dapper = new SqlConnectionFactory<DocenteMateriasGrados>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                dapper.AddParameter("strCC", obj.DocenteID);
                return dapper.Execute(ProcedimientosAlmacenados.CRUD_DOCENTE_MATERIAS_GRADOS);
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { DocenteID = "0", MateriaID = msg + ex.Message };
            }
        }

    }
}