using EduCore.Web.Negocio.Interfaces.Usuario;
using EduCore.Web.Negocio.Usuarios;
using EduCore.Web.Transversales.Entidades.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduCore.Web.BE.Controllers.Usuarios
{
	[ApiController, Route("api/[controller]")]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuariosBLL? _usuarioBLL;

		public UsuarioController(IUsuariosBLL usuarioBLL)
		{
			_usuarioBLL = usuarioBLL ?? throw new ArgumentNullException(nameof(usuarioBLL), "El servicio de usuarios no está inyectado correctamente.");
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Consultar(int usuarioID, string nombreUsuario)
		{
			if (string.IsNullOrEmpty(nombreUsuario) && usuarioID == 0)
			{
				return BadRequest(new { error = "Se debe proporcionar al menos uno de los parámetros: usuarioID o nombreUsuario." });
			}

			Usuario usuario = new()
			{
				UsuarioID = usuarioID,
				NombreUsuario = nombreUsuario
			};

			var response = _usuarioBLL?.Consultar(usuario);

			// Verificamos el ResponseCode correctamente
			return response?.ResponseCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Insertar([FromBody] UsuariosDTO usuario)
		{
			if (usuario == null)
			{
				return BadRequest(new { error = "El modelo de usuario es inválido." });
			}

			var response = _usuarioBLL?.Insertar(usuario);

			// Verificamos el ResponseCode correctamente
			return response?.ResponseCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Actualizar([FromBody] UsuariosDTO usuario)
		{
			if (usuario == null)
			{
				return BadRequest(new { error = "El modelo de usuario es inválido." });
			}

			var response = _usuarioBLL?.Actualizar(usuario);

			// Verificamos el ResponseCode correctamente
			return response?.ResponseCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Eliminar(int usuarioID)
		{
			if (usuarioID == 0)
			{
				return BadRequest(new { error = "El usuarioID es obligatorio." });
			}

			Usuario usuario = new()
			{
				UsuarioID = usuarioID
			};

			var response = _usuarioBLL?.Eliminar(usuario);

			// Verificamos el ResponseCode correctamente
			return response?.ResponseCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}
	}
}
