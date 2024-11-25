using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ConsultarNotasController : ControllerBase
    {
        private readonly IConsultarNotasBLL? _consultarNotasBLL;

        public ConsultarNotasController(IConsultarNotasBLL consultarNotasBLL)
            => _consultarNotasBLL = consultarNotasBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarNotasEstudiantes([FromQuery] string? EstudianteCC = null)
        {
            ConsultarNotas filtro = new()
            {
                EstudianteCC = EstudianteCC
            };
            var response = _consultarNotasBLL?.ConsultarEstudiantesNotas(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarNotasGrados([FromQuery] int GradoID = 0, string? MateriaID = null)
            {
            ConsultarNotas filtro = new()
            {
                GradoID = GradoID,
                MateriaID = MateriaID
            };
            var response = _consultarNotasBLL?.ConsultarGradosNotas(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarGradosDocentes([FromQuery] string? DocenteCC = null)
        {
            ListadoUtilidades filtro = new()
            {
                CC = DocenteCC
            };
            var response = _consultarNotasBLL?.ConsultarGradosDocentes(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarMateriasDocentes([FromQuery] string? DocenteCC = null)
        {
            ListadoUtilidades filtro = new()
            {
                CC = DocenteCC
            };
            var response = _consultarNotasBLL?.ConsultarMateriasDocentes(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
    
}