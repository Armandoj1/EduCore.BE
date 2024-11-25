using EduCore.Web.Transversales.Entidades;

namespace EduCore.Web.Repositorio.Interface;

public interface IEventosDAL
{
    List<Eventos> Consultar(Eventos objInsumo);      
    List<ListadoUtilidades> ConsultarEstudiantes (ListadoUtilidades objInsumo); 
    List<ListadoUtilidades> ConsultarTiposEventos(ListadoUtilidades objInsumo); 
    List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades objInsumo);
    object Eliminar(Eventos objInsumo);                   
    object Insertar(EventosDTO objInsumo);                
    object Actualizar(EventosUpdateDTO objInsumo);              
}