using EduCore.Web.Transversales.Entidades;

namespace EduCore.Web.Repositorio.Interface;

public interface IEventosDAL
{
    List<Eventos> Consultar(Eventos objInsumo);          // Método para consultar eventos
    object Eliminar(Eventos objInsumo);                   // Método para eliminar un evento
    object Insertar(EventosDTO objInsumo);                // Método para insertar un evento
    object Actualizar(EventosDTO objInsumo);              // Método para actualizar un evento
}