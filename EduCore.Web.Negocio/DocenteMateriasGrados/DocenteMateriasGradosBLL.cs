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
    public class DocenteMateriasGradosBLL : IDocenteMateriasGradosBLL
    {
        private readonly IDocenteMateriasGradosDAL _objDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DocenteMateriasGradosBLL(IDocenteMateriasGradosDAL objDAL) => _objDAL = objDAL;

        public TRespuesta<object> Consultar(DocenteMateriasGradosList docenteMateriasGrados)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<DocenteMateriasGradosList> res = _objDAL.Consultar(docenteMateriasGrados);

                if (res != null)
                {
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.DocenteID,
                                                r.NombreCompleto,
                                                r.MateriaID,
                                                r.NombreMateria,
                                                r.GradoID,
                                                r.NombreGrado
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }

                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL");
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

        public TRespuesta<object> ConsultarMaterias (ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.ConsultarMaterias(utilidades);

                if (res != null)
                {

                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.MateriaID,
                                                r.NombreMateria
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

        public TRespuesta<object> ConsultarGrados (ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.ConsultarGrados(utilidades);

                if (res != null)
                {

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
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.UTILIDADES} BLL");
            }
        }

        public TRespuesta<object> Insertar(DocenteMateriasGrados docenteMateriasGrados)
        {
            try
            {
                if (string.IsNullOrEmpty(docenteMateriasGrados.DocenteID) ||
                    string.IsNullOrEmpty(docenteMateriasGrados.MateriaID) ||
                    docenteMateriasGrados.GradoID <= 0)
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Insertar(docenteMateriasGrados);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Actualizar(DocenteMateriasGrados docenteMateriasGrados)
        {
            try
            {
                if (string.IsNullOrEmpty(docenteMateriasGrados.DocenteID) ||
                    string.IsNullOrEmpty(docenteMateriasGrados.MateriaID) ||
                    docenteMateriasGrados.GradoID <= 0)
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Actualizar(docenteMateriasGrados);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Eliminar(DocenteMateriasGrados docenteMateriasGrados)
        {
            try
            {
                if (!string.IsNullOrEmpty(docenteMateriasGrados.DocenteID))
                {
                    var res = _objDAL.Eliminar(docenteMateriasGrados);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { DocenteID = "0", exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTE_MATERIAS_GRADOS} BLL");
            }
        }
    }
}
