using EduCore.Web.Negocio;
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers.Notas
{
	[ApiController, Route("api/[controller]")]
	public class NotasController : ControllerBase
	{
		private readonly INotasBLL? _notasBLL;

		public NotasController(INotasBLL notasBLL) => _notasBLL = notasBLL;

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Insertar([FromBody] Nota nuevaNota)
		{
			var response = _notasBLL?.Insertar(nuevaNota);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

		[ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
		[HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult Actualizar([FromBody] Nota notaActualizada)
		{
			var response = _notasBLL?.Actualizar(notaActualizada);
			return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
		}

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
		public IActionResult HabilitarPeriodo([FromBody] PeriodoVigente periodoVigente)
		{
			var response = _notasBLL?.HabilitarPeriodo(periodoVigente);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarPeriodoVigente([FromQuery] string? PeriodoID = null)
        {
            ListadoUtilidades periodos = new()
            {
                PeriodoVigenteID = Convert.ToInt32(PeriodoID)
            };
            var response = _notasBLL?.ConsultarPeriodoVigente(periodos);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult VerPeriodos([FromQuery] string? PeriodoID = null)
        {
            VerPeriodos periodos = new()
            {
                PeriodoVigenteID = Convert.ToInt32(PeriodoID)
            };

            var response = _notasBLL?.VerPeriodo(periodos);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}