using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface.Notas;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales;
using EduCore.Web.Transversales.Entidades.Notas;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Repositorio.Notas
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

		public List<Nota> Consultar(Nota objInsumo)
		{
			try
			{
				List<Nota> res;
				using DapperManager<Nota> dapper = new SqlConnectionFactory<Nota>(connectionString).GetConnectionManager();
				dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
				dapper.AddParameter("strEstudianteCC", string.IsNullOrEmpty(objInsumo.EstudianteCC) ? null : objInsumo.EstudianteCC);
				dapper.AddParameter("strMateriaID", string.IsNullOrEmpty(objInsumo.MateriaID) ? null : objInsumo.MateriaID);
				dapper.AddParameter("intPeriodoID", objInsumo.PeriodoID == 0 ? null : objInsumo.PeriodoID);

				res = dapper.GetList(ProcedimientosAlmacenados.CRUD_NOTAS).ToList();
				return res;
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new List<Nota> { new() { NotaID = 0, Observacion = msg + ex.Message } };
			}
		}

		public object Insertar(NotaDTO objInsumo)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
					parameters.Add("strEstudianteCC", string.IsNullOrEmpty(objInsumo.EstudianteCC) ? null : objInsumo.EstudianteCC);
					parameters.Add("strMateriaID", string.IsNullOrEmpty(objInsumo.MateriaID) ? null : objInsumo.MateriaID);
					parameters.Add("intPeriodoID", objInsumo.PeriodoID);
					parameters.Add("decimalNotaValor", objInsumo.NotaValor);
					parameters.Add("strObservacion", string.IsNullOrEmpty(objInsumo.Observacion) ? null : objInsumo.Observacion);

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

		public object Actualizar(NotaDTO objInsumo)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
					parameters.Add("strEstudianteCC", string.IsNullOrEmpty(objInsumo.EstudianteCC) ? null : objInsumo.EstudianteCC);
					parameters.Add("strMateriaID", string.IsNullOrEmpty(objInsumo.MateriaID) ? null : objInsumo.MateriaID);
					parameters.Add("intPeriodoID", objInsumo.PeriodoID);
					parameters.Add("decimalNotaValor", objInsumo.NotaValor);
					parameters.Add("strObservacion", string.IsNullOrEmpty(objInsumo.Observacion) ? null : objInsumo.Observacion);

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

		public object Eliminar(Nota objInsumo)
		{
			try
			{
				int res;
				using (DapperManager<Nota> dapper = new SqlConnectionFactory<Nota>(connectionString).GetConnectionManager())
				{
					dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
					dapper.AddParameter("intNotaID", objInsumo.NotaID == 0 ? null : objInsumo.NotaID);

					res = dapper.Execute(ProcedimientosAlmacenados.CRUD_NOTAS);
				}

				return new { filas = res, exitoso = true, error = string.Empty };
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.NOTAS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}
	}
}
