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
                        log.Info($"Evento: {r.EventoID}, Nombre: {r.Titulo}");
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
                                                r.Estado
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

    }
}
