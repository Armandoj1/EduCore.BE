using EduCore.Web.Transversales.Entidades;

namespace EduCore.Web.Repositorio.Interface;

public interface IDatosUsuariosDAL
{
    object ActualizarEstudiante(DatosUsuarios objInsumo);
    object ActualizarDocente(DatosUsuarios objInsumo);
    object ActualizarDirectivo(DatosUsuarios objInsumo);
    object ActualizarContrasena(ActualizarContrasena objInsumo);
}