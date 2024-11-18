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
    public class DocenteEspecialidadBLL : IDocenteEspecialidadBLL
    {
        private readonly IDocenteEspecialidadDAL _objDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DocenteEspecialidadBLL(IDocenteEspecialidadDAL objDAL) => _objDAL = objDAL;

        public TRespuesta<object> Consultar(DocenteEspecialidad docenteEspecialidad)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<DocenteEspecialidad> res = _objDAL.Consultar(docenteEspecialidad);

                if (res != null)
                {
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.EspecialidadID,
                                                r.DocenteID,
                                                r.NombreCompleto,
                                                r.NombreEspecialidad
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }

                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL");
            }
        }

        public TRespuesta<object> Insertar(DocenteEspecialidadDTO docenteEspecialidad)
        {
            try
            {
                if (string.IsNullOrEmpty(docenteEspecialidad.DocenteID) || docenteEspecialidad.EspecialidadID <= 0)
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Insertar(docenteEspecialidad);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Actualizar(DocenteEspecialidadDTO docenteEspecialidad)
        {
            try
            {
                if (string.IsNullOrEmpty(docenteEspecialidad.DocenteID) || docenteEspecialidad.EspecialidadID <= 0)
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Actualizar(docenteEspecialidad);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> ConsultarEspecialidad(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.CosultarEspecialidad(utilidades);

                if (res != null)
                {

                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.EspecialidadID,
                                                r.NombreEspecialidad
                                            }).ToList();

                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }


                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL");
            }
        }

        public TRespuesta<object> ConsultarDocente(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.ConsultarDocente(utilidades);

                if (res != null)
                {

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
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL");
            }
        }

        public TRespuesta<object> Eliminar(DocenteEspecialidadDTO docenteEspecialidad)
        {
            try
            {
                if (docenteEspecialidad.DocenteID != string.Empty)
                {
                    var res = _objDAL.Eliminar(docenteEspecialidad);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { docenteID = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_ESPECIALIDAD} BLL");
            }
        }
    }
}