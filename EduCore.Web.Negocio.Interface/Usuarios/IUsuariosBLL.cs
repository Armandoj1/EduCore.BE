using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Negocio.Interfaces;

public interface IUsuariosBLL
{
	TRespuesta<object> Consultar(UsuariosValidacion objInsumo);
	TRespuesta<object> Insertar(UsuariosDTO objInsumo);
	TRespuesta<object> Actualizar(UsuariosDTO objInsumo);
	TRespuesta<object> Eliminar(Usuarios objInsumo);
}
