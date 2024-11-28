using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IDatosUsuariosBLL
{
    TRespuesta<object> ActualizarDocente(DatosUsuarios objInsumo);
    TRespuesta<object> ActualizarDirectivo(DatosUsuarios objInsumo);
    TRespuesta<object> ActualizarEstudiante(DatosUsuarios objInsumo);
    TRespuesta<object> ActualizarContrasena(ActualizarContrasena objInsumo);
}