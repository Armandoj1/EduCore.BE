using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class EstudiantesController : Controller
    {
        private readonly IEstudiantesBLL? _estudianteBLL;

        public EstudiantesController(IEstudiantesBLL estudianteBLL) => _estudianteBLL = estudianteBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? CC = null)
        {
            Estudiantes estudiante = new()
            {
                CC = CC
            };
            var response = _estudianteBLL?.Consultar(estudiante);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] EstudiantesDTO estudiante)
        {
            var response = _estudianteBLL?.Insertar(estudiante);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar(EstudiantesDTO estudiante)
        {
            var response = _estudianteBLL?.Actualizar(estudiante);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar(string CC)
        {
            Estudiantes estudiante = new()
            {
                CC = CC
            };
            var response = _estudianteBLL?.Eliminar(estudiante);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}