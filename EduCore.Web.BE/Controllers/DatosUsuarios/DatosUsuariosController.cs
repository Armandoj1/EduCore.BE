using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    public class DatosUsuariosController : Controller
    {
        private readonly IDatosUsuariosBLL? _DatosUsuariosBLL;

        public DatosUsuariosController(IDatosUsuariosBLL DatosUsuariosBLL)
            => _DatosUsuariosBLL = DatosUsuariosBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ActualizarDocente([FromBody] DatosUsuarios objInsumo)
        {
            var response = _DatosUsuariosBLL?.ActualizarDocente(objInsumo);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ActualizarDirectivo([FromBody] DatosUsuarios objInsumo)
        {
            var response = _DatosUsuariosBLL?.ActualizarDirectivo(objInsumo);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ActualizarEstudiante([FromBody] DatosUsuarios objInsumo)
        {
            var response = _DatosUsuariosBLL?.ActualizarEstudiante(objInsumo);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult ActualizarContrasena([FromBody] ActualizarContrasena objInsumo)
        {
            var response = _DatosUsuariosBLL?.ActualizarContrasena(objInsumo);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}