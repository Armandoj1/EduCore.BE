using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DocenteEspecialidadController : ControllerBase
    {
        private readonly IDocenteEspecialidadBLL? _docenteEspecialidadBLL;

        public DocenteEspecialidadController(IDocenteEspecialidadBLL docenteEspecialidadBLL)
            => _docenteEspecialidadBLL = docenteEspecialidadBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? docenteID = null)
        {
            DocenteEspecialidad filtro = new()
            {
                DocenteID = docenteID
            };

            var response = _docenteEspecialidadBLL?.Consultar(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]

        public IActionResult ConsultarEspecialidad([FromQuery] int? especialidadID = null)
        {
            ListadoUtilidades filtro = new()
            {
                 EspecialidadID = especialidadID ?? 0   
            };
            var response = _docenteEspecialidadBLL?.ConsultarEspecialidad(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarDocente([FromQuery] string? docenteID = null)
        {
            ListadoUtilidades filtro = new()
            {
                  CC = docenteID 
            };
            var response = _docenteEspecialidadBLL?.ConsultarDocente(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] DocenteEspecialidadDTO docenteEspecialidad)
        {
            var response = _docenteEspecialidadBLL?.Insertar(docenteEspecialidad);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar([FromBody] DocenteEspecialidadDTO docenteEspecialidad)
        {
            var response = _docenteEspecialidadBLL?.Actualizar(docenteEspecialidad);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar([FromQuery] string docenteID, int especialidadID)
        {
            DocenteEspecialidadDTO filtro = new()
            {
                DocenteID = docenteID,
                EspecialidadID = especialidadID
            };

            var response = _docenteEspecialidadBLL?.Eliminar(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}