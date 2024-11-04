using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Transversales.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Web.BE.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicosBLL? _medicoBLL;

        public MedicoController(IMedicosBLL medicoBLL) => _medicoBLL = medicoBLL;

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpGet("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Consultar(int idMedico, string numeroLicencia)
        {
            Medicos medico = new()
            {
                idMedico = idMedico,
                numeroLicencia = numeroLicencia
            };

            var response = _medicoBLL?.Consultar(medico);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPost("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Insertar([FromBody] MedicosDTO medico)
        {
            var response = _medicoBLL?.Insertar(medico);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpPut("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Actualizar(MedicosDTO medico)
        {
            var response = _medicoBLL?.Actualizar(medico);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403)]
        [HttpDelete("[action]"), Produces("application/json", Type = typeof(object))]
        public IActionResult Eliminar(int idMedico)
        {
            Medicos medico = new()
            {
                idMedico = idMedico
            };

            var response = _medicoBLL?.Eliminar(medico);
            return response?.ResponseCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}
