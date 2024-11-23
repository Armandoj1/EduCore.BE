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

    public class GradosMateriasBLL : IGradosMateriasBLL
    {
        private readonly IGradosMateriasDAL _gradosMateriasDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GradosMateriasBLL(IGradosMateriasDAL gradosMateriasDAL) => _gradosMateriasDAL = gradosMateriasDAL;

        public TRespuesta<object> Consultar(GradosMaterias objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<GradosMaterias> res = _gradosMateriasDAL.Consultar(objInsumo);
                if (res != null)
                {
                    foreach (var r in res)
                    {
                        //Con esto haces la prueba en consola para verificar que los datos sean "correctos"
                        log.Info($"Grado: {r.GradoID}, Materia: {r.MateriaID}");
                    }
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.GradoID,
                                                r.NombreGrado,
                                                r.MateriaID,
                                                r.NombreMateria
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.GRADOS_MATERIAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarMaterias(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _gradosMateriasDAL.ConsultarMaterias(utilidades);

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

        public TRespuesta<object> ConsultarGrados(ListadoUtilidades utilidades)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _gradosMateriasDAL.ConsultarGrados(utilidades);

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

        public TRespuesta<Object> Insertar(GradosMateriasDTO objInsumo)
        {
            try
            {
                if (objInsumo.GradoID == 0 || string.IsNullOrEmpty(objInsumo.MateriaID))
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _gradosMateriasDAL.Insertar(objInsumo);

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
                log.Error($"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.GRADOS_MATERIAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_INSERTANDO);
            }
        }

        public TRespuesta<object> Actualizar(GradosMateriasUpdateDTO objInsumo)
        {
            try
            {
                if (objInsumo.GradoID == 0 || string.IsNullOrEmpty(objInsumo.MateriaID))
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
                var res = _gradosMateriasDAL.Actualizar(objInsumo);
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
                log.Error($"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.GRADOS_MATERIAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_ACTUALIZANDO);
            }
        }

        public TRespuesta<object> Eliminar(GradosMaterias objInsumo)
        {
            try
            {
                if (objInsumo.GradoID == 0 || objInsumo.MateriaID != string.Empty)
                {
                    var res = _gradosMateriasDAL.Eliminar(objInsumo);
                    bool procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = true } }
                        : new Collection<object> { new { key = "respuesta", val = new { GradoID = 0, MateriaID = string.Empty, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                {
                    return ResponseManager.ResponseError<object>(Mensajes.INFORMACION_INCOMPLETA);
                }
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.GRADOS_MATERIAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_ELIMINANDO);
            }
        }
    }
}
 