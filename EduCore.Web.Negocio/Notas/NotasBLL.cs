using EduCore.Web.Negocio.Interfaces.Notas;
using EduCore.Web.Repositorio.Interface.Notas;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades.Notas;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Negocio.Notas
{
	public class NotasBLL : INotasBLL
	{
		private readonly INotasDAL _objDAL;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

		public NotasBLL(INotasDAL objDAL) => _objDAL = objDAL;

		public TRespuesta<object> Consultar(Nota objInsumo)
		{
			Collection<object> resCollection = new Collection<object>();
			try
			{
				List<Nota> res = _objDAL.Consultar(objInsumo);

				if (res != null)
				{
					var listadoRespuesta = (from r in res
											select new
											{
												r.NotaID,
												r.EstudianteCC,
												r.MateriaID,
												r.PeriodoID,
												r.NotaValor,
												r.Observacion,
												r.FechaRegistro
											}).ToList();
					resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
				}

				return ResponseManager.ResponseOk(resCollection.Count, resCollection);
			}
			catch (Exception ex)
			{
				log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} BLL: " + ex.Message, ex);
				return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} BLL");
			}
		}

		public TRespuesta<object> Insertar(NotaDTO objInsumo)
		{
			try
			{
				if (string.IsNullOrEmpty(objInsumo.EstudianteCC) || string.IsNullOrEmpty(objInsumo.MateriaID) || objInsumo.PeriodoID == 0 || objInsumo.NotaValor == 0)
				{
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
				}

				var res = _objDAL.Insertar(objInsumo);

				bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));
				string error = res?.GetType().GetProperty("error")?.GetValue(res, null)?.ToString();
				int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null));

				if (!procesoExitoso && !string.IsNullOrEmpty(error))
				{
					return ResponseManager.ResponseError<object>(error);
				}

				return ResponseManager.ResponseOk(filasAfectadas, new Collection<object> { new { key = "respuesta", val = true } });
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.NOTAS} BLL: ";
				log.Error(msg + ex.Message, ex);
				return ResponseManager.ResponseError<object>(msg + ex.Message);
			}
		}

		public TRespuesta<object> Actualizar(NotaDTO objInsumo)
		{
			try
			{
				if (string.IsNullOrEmpty(objInsumo.EstudianteCC) || string.IsNullOrEmpty(objInsumo.MateriaID) || objInsumo.PeriodoID == 0 || objInsumo.NotaValor == 0)
				{
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
				}

				var res = _objDAL.Actualizar(objInsumo);
				bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));
				string error = res?.GetType().GetProperty("error")?.GetValue(res, null)?.ToString();
				int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null));

				if (!procesoExitoso && !string.IsNullOrEmpty(error))
				{
					return ResponseManager.ResponseError<object>(error);
				}

				return ResponseManager.ResponseOk(filasAfectadas, new Collection<object> { new { key = "respuesta", val = true } });
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.NOTAS} BLL: ";
				log.Error(msg + ex.Message, ex);
				return ResponseManager.ResponseError<object>(msg + ex.Message);
			}
		}

		public TRespuesta<object> Eliminar(Nota objInsumo)
		{
			try
			{
				if (objInsumo.NotaID != 0)
				{
					var res = _objDAL.Eliminar(objInsumo);
					var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

					return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
						? new Collection<object> { new { key = "respuesta", val = res } }
						: new Collection<object> { new { key = "respuesta", val = new { NotaID = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
				}
				else
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
			}
			catch (Exception ex)
			{
				log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.NOTAS} BLL: " + ex.Message, ex);
				return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.NOTAS} BLL");
			}
		}
	}
}
