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
    public class MedicosBLL : IMedicosBLL
    {
        private readonly IMedicosDAL _objDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public MedicosBLL(IMedicosDAL objDAL) => _objDAL = objDAL;

        public TRespuesta<object> Consultar(Medicos medico)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<Medicos> res = _objDAL.Consultar(medico);

                if (res != null)
                {
                    var listadoRespuesta = (from r in res
                                            select new
                                            {
                                                r.idMedico,
                                                r.nombreCompleto,
                                                r.numeroLicencia,
                                                r.telefono,
                                                r.correo,
                                                r.direccion,
                                                r.fechaRegistro,
                                                r.fechaModificacion,
                                                r.usuarioRegistro
                                            }).ToList();
                    resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
                }

                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.MEDICOS} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.MEDICOS} BLL");
            }
        }

        public TRespuesta<object> Insertar(MedicosDTO medico)
        {
            try
            {
                if (string.IsNullOrEmpty(medico.nombreCompleto) || string.IsNullOrEmpty(medico.numeroLicencia) ||
                    string.IsNullOrEmpty(medico.telefono) || string.IsNullOrEmpty(medico.correo) ||
                    string.IsNullOrEmpty(medico.direccion))
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Insertar(medico);

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
                string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.MEDICOS} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Actualizar(MedicosDTO medico)
        {
            try
            {
                if (string.IsNullOrEmpty(medico.nombreCompleto) || string.IsNullOrEmpty(medico.numeroLicencia) ||
                    string.IsNullOrEmpty(medico.telefono) || string.IsNullOrEmpty(medico.correo) ||
                    string.IsNullOrEmpty(medico.direccion))
                {
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
                }

                var res = _objDAL.Actualizar(medico);
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
                string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.MEDICOS} BLL: ";
                log.Error(msg + ex.Message, ex);
                return ResponseManager.ResponseError<object>(msg + ex.Message);
            }
        }

        public TRespuesta<object> Eliminar(Medicos medico)
        {
            try
            {
                if (medico.idMedico != 0)
                {
                    var res = _objDAL.Eliminar(medico);
                    var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

                    return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
                        ? new Collection<object> { new { key = "respuesta", val = res } }
                        : new Collection<object> { new { key = "respuesta", val = new { idMedico = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
                }
                else
                    return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.MEDICOS} BLL: " + ex.Message, ex);
                return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.MEDICOS} BLL");
            }
        }
    }
}
