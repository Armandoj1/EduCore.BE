using EduCore.Web.Negocio.Interfaces.Notas;
using EduCore.Web.Transversales.Entidades.Notas;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers.Notas
{
	[ApiController, Route("api/[controller]")]
	public class NotasController : ControllerBase
	{
		private readonly INotasBLL? _notasBLL;

		public NotasController(INotasBLL notasBLL) => _notasBLL = notasBLL;

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Consultar(int? notaID)
		{
			// Verificar si el parametro notaID es null, si es así devolver BadRequest
			if (!notaID.HasValue)
			{
				return BadRequest(new { message = "notaID no puede ser null" });
			}

			// Crear un objeto Nota con el notaID
			var nota = new Nota { NotaID = notaID.Value };

			// Llamar al método Consultar de la capa de negocio
			var response = _notasBLL?.Consultar(nota);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Insertar([FromBody] NotaDTO nuevaNota)
		{
			var response = _notasBLL?.Insertar(nuevaNota);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Actualizar([FromBody] NotaDTO notaActualizada)
		{
			var response = _notasBLL?.Actualizar(notaActualizada);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Eliminar(int notaID)
		{
			// Crear un objeto Nota con el notaID
			var nota = new Nota { NotaID = notaID };

			// Llamar al método Eliminar de la capa de negocio
			var response = _notasBLL?.Eliminar(nota);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}
	}
}
