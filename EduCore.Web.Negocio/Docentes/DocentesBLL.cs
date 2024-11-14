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
    public class DocentesBLL : IDocentesBLL
    {
        private readonly IDocentesDAL _objDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public DocentesBLL(IDocentesDAL objDAL) => _objDAL = objDAL;

        public TRespuesta<object> Consultar(Docentes docente)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<Docentes> res = _objDAL.Consultar(docente);

                if (res != null)
                {
                    foreach (var r in res)
                    {
                        log.Info($"CC: {r.CC}, EspecialidadID: {r.EspecialidadID}, GradoID: {r.GradoID}, MateriaID: {r.MateriaID}");
                    }

                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.CC,
                                                r.NombreCompleto,
                                                r.Telefono,
                                                r.Correo,
                                                r.Direccion,
                                                r.FechaNacimiento,
                                                r.edad,
                                                r.Especialidad,
                                                r.Grado,
                                                r.Materia
                                            }).ToList();

                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }


                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.DOCENTES} BLL");
            }
        }

        public TRespuesta<object> Consultar(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.Consultar(utilidades);

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

        public TRespuesta<object> ConsultarGrado(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _objDAL.ConsultarGrado(utilidades);

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

        public TRespuesta<object> Insertar(DocentesDTO docente)
        {
            try
            {
                if (string.IsNullOrEmpty(docente.NombreCompleto) || string.IsNullOrEmpty(docente.Telefono) ||
                    string.IsNullOrEmpty(docente.Correo) || string.IsNullOrEmpty(docente.Direccion) || string.IsNullOrEmpty(docente.CC) )
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Insertar(docente);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.DOCENTES} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Actualizar(DocentesDTO docente)
        {
            try
            {
                if (string.IsNullOrEmpty(docente.NombreCompleto) || string.IsNullOrEmpty(docente.Telefono) ||
                    string.IsNullOrEmpty(docente.Correo) || string.IsNullOrEmpty(docente.Direccion) || string.IsNullOrEmpty(docente.CC))
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Actualizar(docente);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.DOCENTES} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Eliminar(Docentes docente)
        {
            try
            {
                if (docente.CC != string.Empty)
                {
                    var res = _objDAL.Eliminar(docente);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { CC = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.DOCENTES} BLL");
            }
        }
    }
}
