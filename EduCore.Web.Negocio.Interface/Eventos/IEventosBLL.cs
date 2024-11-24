using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IEventosBLL
{
    TRespuesta<object> Consultar(Eventos objInsumo);
    TRespuesta<object> Insertar(EventosDTO objInsumo);
    TRespuesta<object> Actualizar(EventosDTO objInsumo);
    TRespuesta<object> Eliminar(Eventos objInsumo);
}