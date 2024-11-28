
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DocenteMateriasGradosController : ControllerBase
    {
        private readonly IDocenteMateriasGradosBLL? _docenteMateriasGradosBLL;

        public DocenteMateriasGradosController(IDocenteMateriasGradosBLL docenteMateriasGradosBLL)  
            => _docenteMateriasGradosBLL = docenteMateriasGradosBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? docenteID = null)
        {
            DocenteMateriasGradosList filtro = new()
            {
                DocenteID = docenteID
            };

            var response = _docenteMateriasGradosBLL?.Consultar(filtro);
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
            var response = _docenteMateriasGradosBLL?.ConsultarDocente(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarMaterias([FromQuery] string? MateriaID = null)
        {
            ListadoUtilidades filtro = new()
            {
                MateriaID = MateriaID
            };
            var response = _docenteMateriasGradosBLL?.ConsultarMaterias(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarGrados([FromQuery] string? GradoID = null)
        {
            ListadoUtilidades filtro = new()
            {
                GradoID = Convert.ToInt32(GradoID)
            };
            var response = _docenteMateriasGradosBLL?.ConsultarGrados(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] DocenteMateriasGrados docenteMateriasGrados)
        {
            var response = _docenteMateriasGradosBLL?.Insertar(docenteMateriasGrados);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar([FromBody] DocenteMateriasGrados docenteMateriasGrados)
        {
            var response = _docenteMateriasGradosBLL?.Actualizar(docenteMateriasGrados);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar([FromQuery] string docenteID, string materiaID, int gradoID)
        {
            DocenteMateriasGrados filtro = new()
            {
                DocenteID = docenteID,
                MateriaID = materiaID,
                GradoID = gradoID
            };

            var response = _docenteMateriasGradosBLL?.Eliminar(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}