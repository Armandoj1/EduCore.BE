using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IUsuariosDAL
{
	List<UsuariosValidacion> Consultar(UsuariosValidacion objInsumo);          // Método para consultar usuarios
	object Eliminar(Usuarios objInsumo);                   // Método para eliminar un usuario
	object Insertar(UsuariosDTO objInsumo);                // Método para insertar un usuario
	object Actualizar(UsuariosDTO objInsumo);              // Método para actualizar un usuario
}