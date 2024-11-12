using EduCore.Web.Transversales.Entidades.Usuarios;
using EduCore.Web.Transversales.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Negocio.Interfaces.Usuario
{
	public interface IUsuariosBLL
	{
		TRespuesta<object> Consultar(EduCore.Web.Transversales.Entidades.Usuarios.Usuario objInsumo);
		TRespuesta<object> Insertar(UsuariosDTO objInsumo);
		TRespuesta<object> Actualizar(UsuariosDTO objInsumo);
		TRespuesta<object> Eliminar(EduCore.Web.Transversales.Entidades.Usuarios.Usuario objInsumo);
	}
}
