using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Entidades;
using log4net;

namespace EduCore.Web.Repositorio
{
    public class ConsultarNotasDAL : IConsultarNotasDAL
    {
        private readonly string _connectionString;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ConsultarNotasDAL()
        {
            var objConfig = new Config();
            _connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
        }

        public List<ConsultarNotas> ConsultarEstudiantesNotas(ConsultarNotas objInsumo)
        {
            try
            {
                List<ConsultarNotas> res;
                using DapperManager<ConsultarNotas> dapper = new SqlConnectionFactory<ConsultarNotas>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
                dapper.AddParameter("EstudianteCC", objInsumo.EstudianteCC);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_CONSULTAR_NOTAS).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ConsultarNotas> { new() { EstudianteCC = string.Empty, NombreEstudiante = msg + ex.Message } };
            }
        }

        public List<ConsultarNotas> ConsultarGradosNotas(ConsultarNotas objInsumo)
        {
            try
            {
                List<ConsultarNotas> res;
                using DapperManager<ConsultarNotas> dapper = new SqlConnectionFactory<ConsultarNotas>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 2);
                dapper.AddParameter("GradoID", objInsumo.GradoID);
                dapper.AddParameter("MateriaID", objInsumo.MateriaID);
                res = dapper.GetList(ProcedimientosAlmacenados.CRUD_CONSULTAR_NOTAS).ToList();
                return res;

            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ConsultarNotas> { new() { EstudianteCC = string.Empty, NombreEstudiante = msg + ex.Message } };
            }

        }

        public List<ListadoUtilidades> ConsultarGradosDocentes(ListadoUtilidades objInsumo)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 8);
                dapper.AddParameter("strCC", objInsumo.CC);
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

        public List<ListadoUtilidades> ConsultarMateriasDocentes(ListadoUtilidades objInsumo)
        {
            try
            {
                List<ListadoUtilidades> res;
                using DapperManager<ListadoUtilidades> dapper = new SqlConnectionFactory<ListadoUtilidades>(_connectionString).GetConnectionManager();
                dapper.AddParameter("intOpcion", 9);
                dapper.AddParameter("strCC", objInsumo.CC);
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

    }
}