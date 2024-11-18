using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DocenteController : ControllerBase
    {
        private readonly IDocentesBLL? _docenteBLL;

        public DocenteController(IDocentesBLL docenteBLL) => _docenteBLL = docenteBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? CC = null)
        {
            Docentes docente = new()
            {
                CC = CC
            };

            var response = _docenteBLL?.Consultar(docente);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] DocentesDTO docente)
        {
            var response = _docenteBLL?.Insertar(docente);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar(DocentesDTO docente)
        {
            var response = _docenteBLL?.Actualizar(docente);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar(string CC)
        {
            Docentes docente = new()
            {
                CC = CC
            };

            var response = _docenteBLL?.Eliminar(docente);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}
