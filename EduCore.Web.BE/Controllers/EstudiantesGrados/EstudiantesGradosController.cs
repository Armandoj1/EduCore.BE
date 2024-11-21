using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class EstudiantesGradosController : Controller
    {
        private readonly IEstudiantesGradosBLL? _estudiantesGradosBLL;
        public EstudiantesGradosController(IEstudiantesGradosBLL estudiantesGradosBLL) => _estudiantesGradosBLL = estudiantesGradosBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? EstudianteCC = null)
        {
            EstudiantesGrados estudianteGrado = new()
            {
                EstudianteCC = EstudianteCC
            };
            var response = _estudiantesGradosBLL?.Consultar(estudianteGrado);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarGrados([FromQuery] string? CC = null)
        {
            ListadoUtilidades filtro = new()
            {
                CC  = CC
            };
            var response = _estudiantesGradosBLL?.ConsultarGrados(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarEstudiantes([FromQuery] string? CC = null)
        {
            ListadoUtilidades filtro = new()
            {
                CC = CC
            };
            var response = _estudiantesGradosBLL?.ConsultarEstudiantes(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] EstudiantesGradosDTO estudianteGrado)
        {
            var response = _estudiantesGradosBLL?.Insertar(estudianteGrado);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }


        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar(EstudiantesGradosDTO estudianteGrado)
        {
            var response = _estudiantesGradosBLL?.Actualizar(estudianteGrado);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar(string EstudianteCC, int GradoID)
        {
            EstudiantesGrados estudianteGrado = new()
            {
                EstudianteCC = EstudianteCC,
                GradoID = GradoID
            };
            var response = _estudiantesGradosBLL?.Eliminar(estudianteGrado);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}