using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Entidades;

namespace EduCore.Web.Repositorio
{
	public class NotasDAL : INotasDAL
	{
		private string connectionString;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

		public NotasDAL()
		{
			var objConfig = new Config();
			connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
		}

		public object Insertar(Nota objInsumo)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", 1);
					parameters.Add("EstudianteCC", string.IsNullOrEmpty(objInsumo.EstudianteCC) ? null : objInsumo.EstudianteCC);
					parameters.Add("MateriaID", string.IsNullOrEmpty(objInsumo.MateriaID) ? null : objInsumo.MateriaID);
					parameters.Add("PeriodoID", objInsumo.PeriodoID);
					parameters.Add("Nota", objInsumo.NotaValor);

					var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_NOTAS, parameters, commandType: CommandType.StoredProcedure);

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
				string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.NOTAS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}

		public object Actualizar(Nota objInsumo)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", 2);
					parameters.Add("EstudianteCC", string.IsNullOrEmpty(objInsumo.EstudianteCC) ? null : objInsumo.EstudianteCC);
					parameters.Add("MateriaID", string.IsNullOrEmpty(objInsumo.MateriaID) ? null : objInsumo.MateriaID);
					parameters.Add("PeriodoID", objInsumo.PeriodoID);
					parameters.Add("Nota", objInsumo.NotaValor);

					var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_NOTAS, parameters, commandType: CommandType.StoredProcedure);

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
				string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.NOTAS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}
	
		public object HabilitarPeriodo(PeriodoVigente periodoVigente)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", 3);
					parameters.Add("PeriodoVigenteID", periodoVigente.PeriodoVigenteID);
					parameters.Add("Estado", periodoVigente.Estado);
					var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_NOTAS, parameters, commandType: CommandType.StoredProcedure);
					return result;

				}

			}

            catch (Exception ex)
			{
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.NOTAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new { filas = 0, exitoso = false, error = msg + ex.Message };
            }
		}

        public List<ListadoUtilidades> ConsultarPeriodoVigente(ListadoUtilidades objInsumo)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", 10);
                    var result = connection.Query<ListadoUtilidades>(ProcedimientosAlmacenados.CRUD_UTILIDADES, parameters, commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<ListadoUtilidades>();
            }
        }

        public List<VerPeriodos> VerPeriodo(VerPeriodos objInsumo)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("intOpcion", 11);
                    var result = connection.Query<VerPeriodos>(ProcedimientosAlmacenados.CRUD_UTILIDADES, parameters, commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} DAL: ";
                log.Error(msg + ex.Message, ex);
                return new List<VerPeriodos>();
            }
        }

    }
}