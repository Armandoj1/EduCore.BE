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
    public class EventosBLL : IEventosBLL
    {
        private readonly IEventosDAL _eventosDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EventosBLL(IEventosDAL eventosDAL) => _eventosDAL = eventosDAL;

        public TRespuesta<object> Consultar(Eventos objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<Eventos> res = _eventosDAL.Consultar(objInsumo);
                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"Evento: {r.Titulo}, Nombre: {r.Descripcion}");
                    }
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.EventoID,
                                                r.Titulo,
                                                r.Descripcion,
                                                r.FechaInicio,
                                                r.FechaFin,
                                                r.GradoID,
                                                r.Estado,
                                                r.TipoEvento,
                                                r.Nombre,
                                                r.UsuarioCreadorID
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarEstudiantes(ListadoUtilidades objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _eventosDAL.ConsultarEstudiantes(objInsumo);
                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"Cédula: {r.EstudianteCC}, Nombre: {r.NombreCompleto}");
                    }
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.CC,
                                                r.NombreCompleto
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarTiposEventos(ListadoUtilidades objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _eventosDAL.ConsultarTiposEventos(objInsumo);
                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"TipoEventoID: {r.TipoEventoID}, Nombre: {r.Nombre}");
                    }
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.TipoEventoID,
                                                r.Nombre
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarGrados(ListadoUtilidades objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _eventosDAL.ConsultarGrados(objInsumo);
                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"TipoEventoID: {r.GradoID}, Nombre: {r.NombreGrado}");
                    }
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.GradoID,
                                                r.NombreGrado
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> Insertar(EventosDTO objInsumo)
        {
            try
            {
                if(objInsumo == null)
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _eventosDAL.Insertar(objInsumo);
                bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));
                string error = res?.GetType().GetProperty("error")?.GetValue(res, null).ToString();
                int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null));

                if (!procesoExitoso && !string.IsNullOrEmpty(error))
                {
                    return ResponseManager.ResponseError<object>(error);
                }

                return ResponseManager.ResponseOk(filasAfectadas, new Collection<object>() { new { key = "respuesta", val = true } });
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_INSERTANDO);
            }
        }


        public TRespuesta<object> Actualizar (EventosUpdateDTO objInsumo)
        {
            try
            {
                if (objInsumo == null)
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _eventosDAL.Actualizar(objInsumo);
                bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));
                string error = res?.GetType().GetProperty("error")?.GetValue(res, null).ToString();
                int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null));

                if (!procesoExitoso && !string.IsNullOrEmpty(error))
                {
                    return ResponseManager.ResponseError<object>(error);
                }

                return ResponseManager.ResponseOk(filasAfectadas, new Collection<object>() { new { key = "respuesta", val = true } });

            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_ACTUALIZANDO);
            }   
        }

        public TRespuesta<object> Eliminar(Eventos objInsumo)
        {
            try
            {
                if (objInsumo.EventoID != 0)
                {
                    var res = _eventosDAL.Eliminar(objInsumo);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso").GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { CC = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
            
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.EVENTOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_ELIMINANDO);
            }
        }
    }
}