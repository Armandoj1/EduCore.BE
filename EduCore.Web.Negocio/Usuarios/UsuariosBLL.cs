using EduCore.Web.Negocio.Interfaces.Usuario;
using EduCore.Web.Repositorio.Interface.Usuarios;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades.Usuarios;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Negocio.Usuarios
{
	public class UsuariosBLL : IUsuariosBLL
	{
		private readonly IUsuariosDAL _objDAL;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

		public UsuariosBLL(IUsuariosDAL objDAL) => _objDAL = objDAL;

		public TRespuesta<object> Consultar(Usuario usuario)
		{
			Collection<object> resCollection = new Collection<object>();
			try
			{
				List<Usuario> res = _objDAL.Consultar(usuario);

				if (res != null)
				{
					var listadoRespuesta = (from r in res
											select new
											{
												r.UsuarioID,
												r.NombreUsuario,
												r.Contrasena
											}).ToList();
					resCollection = new Collection<object>(listadoRespuesta.Cast<object>().ToList());
				}

				return ResponseManager.ResponseOk(resCollection.Count, resCollection);
			}
			catch (Exception ex)
			{
				log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.USUARIOS} BLL: " + ex.Message, ex);
				return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.USUARIOS} BLL");
			}
		}

		public TRespuesta<object> Insertar(UsuariosDTO usuario)
		{
			try
			{
				if (string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Contrasena))
				{
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
				}

				var res = _objDAL.Insertar(usuario);

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
				string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.USUARIOS} BLL: ";
				log.Error(msg + ex.Message, ex);
				return ResponseManager.ResponseError<object>(msg + ex.Message);
			}
		}

		public TRespuesta<object> Actualizar(UsuariosDTO usuario)
		{
			try
			{
				if (string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Contrasena))
				{
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
				}

				var res = _objDAL.Actualizar(usuario);
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
				string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.USUARIOS} BLL: ";
				log.Error(msg + ex.Message, ex);
				return ResponseManager.ResponseError<object>(msg + ex.Message);
			}
		}

		public TRespuesta<object> Eliminar(Usuario usuario)
		{
			try
			{
				if (usuario.UsuarioID != 0)
				{
					var res = _objDAL.Eliminar(usuario);
					var procesoExitoso = Convert.ToBoolean(res?.GetType().GetProperty("exitoso")?.GetValue(res, null));

					return ResponseManager.ResponseOk(Convert.ToInt32(res?.GetType().GetProperty("filas")?.GetValue(res, null)), procesoExitoso
						? new Collection<object> { new { key = "respuesta", val = res } }
						: new Collection<object> { new { key = "respuesta", val = new { UsuarioID = 0, exitoso = false, error = Mensajes.INFORMACION_INCOMPLETA } } });
				}
				else
					return ResponseManager.ResponseValidation<object>(Mensajes.INFORMACION_INCOMPLETA);
			}
			catch (Exception ex)
			{
				log.Error($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.USUARIOS} BLL: " + ex.Message, ex);
				return ResponseManager.ResponseError<object>($"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.USUARIOS} BLL");
			}
		}
	}

}
