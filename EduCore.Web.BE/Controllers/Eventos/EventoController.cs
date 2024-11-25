using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class EventoController : Controller
    {
        private readonly IEventosBLL? _eventoBLL;
        public EventoController(IEventosBLL eventoBLL) => _eventoBLL = eventoBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar([FromQuery] string? EventoCreadorID = null)
        {
            Eventos evento = new()
            {
                UsuarioCreadorID = EventoCreadorID
            };
            var response = _eventoBLL?.Consultar(evento);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarEstudiantes([FromQuery] string? EstudianteCC = null)
        {
            ListadoUtilidades filtro = new()
            {
                CC = EstudianteCC
            };
            var response = _eventoBLL?.ConsultarEstudiantes(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarTiposEventos([FromQuery] int TipoEventoID = 0)
        {
            ListadoUtilidades filtro = new()
            {
                TipoEventoID = TipoEventoID
            };
            var response = _eventoBLL?.ConsultarTiposEventos(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ConsultarGrados([FromQuery] int GradoID = 0)
        {
            ListadoUtilidades filtro = new()
            {
                GradoID = GradoID
            };
            var response = _eventoBLL?.ConsultarGrados(filtro);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] EventosDTO evento)
        {
            var response = _eventoBLL?.Insertar(evento);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar([FromBody] EventosUpdateDTO evento)
        {
            var response = _eventoBLL?.Actualizar(evento);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar([FromQuery] int EventoID = 0)
        {
            Eventos evento = new()
            {
                EventoID = EventoID
            };
            var response = _eventoBLL?.Eliminar(evento);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);

        }
    }
}