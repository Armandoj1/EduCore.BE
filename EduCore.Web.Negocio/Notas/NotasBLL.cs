using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EduCore.Web.Negocio
{
	public class NotasBLL : INotasBLL
	{
		private readonly INotasDAL _objDAL;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

		public NotasBLL(INotasDAL objDAL) => _objDAL = objDAL;

		public TRespuesta<object> Insertar(Nota objInsumo)
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

		public TRespuesta<object> Actualizar(Nota objInsumo)
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

		public TRespuesta<object> HabilitarPeriodo(PeriodoVigente periodoVigente)
		{
			try
			{
				if (periodoVigente.Estado == 0 || periodoVigente.PeriodoVigenteID == 0)
				{
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
				
				var res = _objDAL.HabilitarPeriodo(periodoVigente);
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

		public TRespuesta<object> ConsultarPeriodoVigente(ListadoUtilidades objInsumo)
		{
			try
			{
				List<ListadoUtilidades> res = _objDAL.ConsultarPeriodoVigente(objInsumo);
				Collection<object> resCollection = new Collection<object>();
				if (res != null)
				{
					var consultarNotas = (from r in res
										  select new
										  {
											  r.PeriodoVigenteID,
											  r.NombrePeriodo
										  }).ToList();
					resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
				}
				return ResponseManager.ResponseOk(resCollection.Count, resCollection);
			}
			catch (Exception ex)
			{
				log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} BLL: {ex.Message}", ex);
				return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
			}

		}

        public TRespuesta<object> VerPeriodo(VerPeriodos objInsumo)
        {
            try
            {
                List<VerPeriodos> res = _objDAL.VerPeriodo(objInsumo);
                Collection<object> resCollection = new Collection<object>();
                if (res != null)
                {
                    var consultarNotas = (from r in res
                                          select new
                                          {
                                              r.PeriodoVigenteID,
                                              r.NombrePeriodo, 
											  r.Estado
                                          }).ToList();
                    resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.NOTAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }

        }
    }
}