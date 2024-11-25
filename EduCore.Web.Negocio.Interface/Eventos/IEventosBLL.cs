using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IEventosBLL
{
    TRespuesta<object> Consultar(Eventos objInsumo);
    TRespuesta<object> ConsultarEstudiantes(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarTiposEventos(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarGrados(ListadoUtilidades objInsumo);
    TRespuesta<object> Insertar(EventosDTO objInsumo);
    TRespuesta<object> Actualizar(EventosUpdateDTO objInsumo);
    TRespuesta<object> Eliminar(Eventos objInsumo);
}