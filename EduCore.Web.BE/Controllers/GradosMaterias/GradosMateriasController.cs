using EduCore.Web.Negocio;
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class GradosMateriasController : Controller
    {

        private readonly IGradosMateriasBLL? _gradosMateriasBLL;
        public GradosMateriasController(IGradosMateriasBLL gradosMateriasBLL) => _gradosMateriasBLL = gradosMateriasBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? GradoID = null)
        {
            GradosMaterias gradoMateria = new()
            {
                GradoID = Convert.ToInt32(GradoID)
            };
            var response = _gradosMateriasBLL?.Consultar(gradoMateria);
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
            var response = _gradosMateriasBLL?.ConsultarMaterias(filtro);
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
            var response = _gradosMateriasBLL?.ConsultarGrados(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] GradosMateriasDTO gradoMateria)
        {
            var response = _gradosMateriasBLL?.Insertar(gradoMateria);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar(GradosMateriasUpdateDTO gradoMateria)
        {
            var response = _gradosMateriasBLL?.Actualizar(gradoMateria);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar([FromQuery] int GradoID = 0, [FromQuery] string? MateriaID = null)
        {
            GradosMaterias gradoMateria = new()
            {
                GradoID = GradoID,
                MateriaID = MateriaID
            };
            var response = _gradosMateriasBLL?.Eliminar(gradoMateria);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}