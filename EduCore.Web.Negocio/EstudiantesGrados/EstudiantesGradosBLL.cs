using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Reflection;

namespace EduCore.Web.Negocio
{
    public class EstudiantesGradosBLL : IEstudiantesGradosBLL
    {

        private readonly IEstudiantesGradosDAL _estudiantesGradosDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EstudiantesGradosBLL(IEstudiantesGradosDAL estudiantesGradosDAL) => _estudiantesGradosDAL = estudiantesGradosDAL;

        public TRespuesta<object> Consultar(EstudiantesGrados objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<EstudiantesGrados> res = _estudiantesGradosDAL.Consultar(objInsumo);
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
                                                r.EstudianteCC,
                                                r.NombreCompleto,
                                                r.GradoID,
                                                r.NombreGrado
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.ESTUDIANTES_GRADOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarGrados(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _estudiantesGradosDAL.ConsultarGrados(utilidades);

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

        public TRespuesta<object> ConsultarEstudiantes(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _estudiantesGradosDAL.ConsultarEstudiantes(utilidades);

                if (res != null)
                {

                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.NombreCompleto,
                                                r.EstudianteCC
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

        public TRespuesta<object> Insertar(EstudiantesGradosDTO objInsumo)
        {
            try
            {
                if (string.IsNullOrEmpty(objInsumo.EstudianteCC) || objInsumo.GradoID == 0)
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _estudiantesGradosDAL.Insertar(objInsumo);

                bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso").GetValue(res, null));
                string error = res?.GetType().GetProperty("error").GetValue(res, null)?.ToString();
                int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas").GetValue(res, null));

                if (!procesoExitoso && !string.IsNullOrEmpty(error))
                {
                    return ResponseManager.ResponseError<object>(error);
                }

                return ResponseManager.ResponseOk(filasAfectadas, new Collection<object>() { new { key = "respuesta", val = true } });


            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.ESTUDIANTES_GRADOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_INSERTANDO);
            }
        }

        public TRespuesta<object> Actualizar(EstudiantesGradosDTO estudiantes)
        {
            try
            {

                if (string.IsNullOrEmpty(estudiantes.EstudianteCC) || estudiantes.GradoID == 0)
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
                var res = _estudiantesGradosDAL.Actualizar(estudiantes);
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
                log.Error($"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.ESTUDIANTES_GRADOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_ACTUALIZANDO);
            }
        }

        public TRespuesta<object> Eliminar(EstudiantesGrados estudiantes)
        {
            try
            {
                if (estudiantes.EstudianteCC != string.Empty && estudiantes.GradoID != 0)
                {
                    var res = _estudiantesGradosDAL.Eliminar(estudiantes);
                    bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { EstudianteCC = 0, GradoID = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.ESTUDIANTES_GRADOS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO}{Funcionalidades.ESTUDIANTES_GRADOS} BLL:");
            }
        }
    }
}