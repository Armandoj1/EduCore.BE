using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduCore.Web.Transversales.Entidades.Usuarios;

namespace EduCore.Web.Repositorio.Interface.Usuarios
{
	public interface IUsuariosDAL
	{
		List<Usuario> Consultar(Usuario objInsumo);          // Método para consultar usuarios
		object Eliminar(Usuario objInsumo);                   // Método para eliminar un usuario
		object Insertar(UsuariosDTO objInsumo);                // Método para insertar un usuario
		object Actualizar(UsuariosDTO objInsumo);              // Método para actualizar un usuario
	}
}
