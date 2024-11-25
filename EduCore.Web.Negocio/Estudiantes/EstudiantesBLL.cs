using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace EduCore.Web.Negocio
{
    public class EstudiantesBLL : IEstudiantesBLL
    {
        private readonly IEstudiantesDAL _objDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EstudiantesBLL(IEstudiantesDAL objDAL) => _objDAL = objDAL;

        public TRespuesta<object> Consultar(Estudiantes objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<Estudiantes> res = _objDAL.Consultar(objInsumo);

                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"Cédula: {r.CC}, Nombre: {r.NombreCompleto}");
                    }

                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.CC,
                                                r.NombreCompleto,
                                                r.FechaNacimiento,
                                                r.Direccion,
                                                r.edad,
                                                r.Telefono,
                                                r.Correo,
                                                r.GradoID,
                                                r.NombreGrado
                                            }).ToList();

                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());

                }

                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.ESTUDIANTES} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> Insertar(EstudiantesDTO estudiantes)
        {
            try
            {
                if (string.IsNullOrEmpty(estudiantes.CC) || string.IsNullOrEmpty(estudiantes.NombreCompleto) || string.IsNullOrEmpty(estudiantes.Telefono) ||
                    string.IsNullOrEmpty(estudiantes.Direccion) || string.IsNullOrEmpty(Convert.ToString(estudiantes.FechaNacimiento)) || string.IsNullOrEmpty(estudiantes.Correo))
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Insertar(estudiantes);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.ESTUDIANTES} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Actualizar(EstudiantesDTO estudiantes)
        {
            try
            {
                if (string.IsNullOrEmpty(estudiantes.CC) || string.IsNullOrEmpty(estudiantes.NombreCompleto) || string.IsNullOrEmpty(estudiantes.Telefono) ||
                    string.IsNullOrEmpty(estudiantes.Direccion) || string.IsNullOrEmpty(Convert.ToString(estudiantes.FechaNacimiento)))
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
                var res = _objDAL.Actualizar(estudiantes);
                bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));
                string error = res?.GetType().GetProperty("error")?.GetValue(res, null).ToString();
                int filasAfectadas = Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null));

                if (!procesoExitoso && !string.IsNullOrEmpty(error))
                {
                    return ResponseManager.ResponseError<object>(error);
                }

                return ResponseManager.ResponseOk(filasAfectadas, new Collection<object> { new { key = "respuesta", val = true } });

            }
            catch (Exception ex)
            {
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.ESTUDIANTES} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }


        public TRespuesta<object> Eliminar(Estudiantes estudiantes)
        {
            try
            {
                if (estudiantes.CC != string.Empty)
                {
                    var res = _objDAL.Eliminar(estudiantes);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { CC = 0, exitoso = false, error = string.Empty } } });
                }
                else
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.ESTUDIANTES} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.ESTUDIANTES} BLL");
            }

        }
    }
}