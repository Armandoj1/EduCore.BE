using EduCore.Web.Data;
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
using OfficeOpenXml.Utils;
using System.Reflection.Metadata;

namespace EduCore.Web.Repositorio
{
    public class GradosMateriasDAL : IGradosMateriasDAL
    {

        public readonly string connectionString;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public GradosMateriasDAL()
        {
            var objConfig = new Config();
            connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<GradosMaterias> Consultar(GradosMaterias objInsumo)
        {
            try
            {
                List<GradosMaterias> res;
                using DapperManager<GradosMaterias> dapper = new SqlConnectionFactory<GradosMaterias>(connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("intGradoID", objInsumo.GradoID);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_GRADOS_MATERIAS).ToList();
                return res;

            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.GRADOS_MATERIAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<GradosMaterias> { new() { GradoID = 0, MateriaID = string.Empty, NombreGrado = msg + ex.Message } };
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

        public object Insertar(GradosMateriasDTO objInsumo)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    paramaters.Add("intGradoID", objInsumo.GradoID);
                    paramaters.Add("strMateriaID", objInsumo.MateriaID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_GRADOS_MATERIAS, paramaters, commandType: CommandType.StoredProcedure);

                    if (result != null && (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302 ))
                    {
                        return new { filas = 0, exitoso = false, error = result.reponseMessage };
                    }

                    int filas = result?.filas ?? 0;
                    return new { filas = filas, exitoso = true, error = string.Empty };

                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.GRADOS_MATERIAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { Error = msg + ex.Message };
            }
        }

        public object Actualizar(GradosMateriasUpdateDTO objInsumo)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                    parameters.Add("strMateriaID", objInsumo.MateriaID);
                    parameters.Add("intGradoID", objInsumo.GradoID);
                    parameters.Add("strNuevaMateriaID", objInsumo.NuevaMateriaID);
                    var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_GRADOS_MATERIAS, parameters, commandType: CommandType.StoredProcedure);

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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.GRADOS_MATERIAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { Error = msg + ex.Message };
            }
        }

        public object Eliminar(GradosMaterias objInsumo)
        {
            try
            {
                int res;
                using (DapperManager<GradosMaterias> Dapper = new SqlConnectionFactory<GradosMaterias>(connectionString).GetConnectionManager())
                {
                    Dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                    Dapper.AddParameter("intGradoID", objInsumo.GradoID);
                    Dapper.AddParameter("strMateriaID", objInsumo.MateriaID);
                    res = Dapper.Execute(ProcedimientosAlmacenados.CRUD_GRADOS_MATERIAS);
                }

                return new { filas = res, exitoso = true, error = string.Empty };
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.GRADOS_MATERIAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { Error = msg + ex.Message };
            }

        }
    }
}