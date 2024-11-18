using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Entidades;
using log4net;
using System.Reflection;

namespace EduCore.Web.Repositorio {
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
                dapper.AddParameter("CC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
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

        public object Insertar(Estudiantes obj)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                    parameters.Add("CC", string.IsNullOrEmpty(obj.CC) ? null : obj.CC);
                    parameters.Add("StrNombreCompleto", string.IsNullOrEmpty(obj.NombreCompleto) ? null : obj.NombreCompleto);
                    parameters.Add("StrDireccion", string.IsNullOrEmpty(obj.Direccion) ? null : obj.Direccion);
                    parameters.Add("StrTelefono", string.IsNullOrEmpty(obj.Telefono) ? null : obj.Telefono);
                    parameters.Add("StrCorreo", string.IsNullOrEmpty(obj.Correo) ? null : obj.Correo);
                    parameters.Add("StrFechaNacimiento", string.IsNullOrEmpty(obj.FechaNacimiento) ? null : obj.FechaNacimiento);
                }
            }

        }
    }
}