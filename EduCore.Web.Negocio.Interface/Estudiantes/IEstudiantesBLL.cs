using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IEstudiantesBLL
{
    TRespuesta<object> Consultar(Estudiantes objInsumo);
    TRespuesta<object> Insertar(EstudiantesDTO objInsumo);
    TRespuesta<object> Actualizar(EstudiantesDTO objInsumo);
    TRespuesta<object> Eliminar(Estudiantes objInsumo);
}