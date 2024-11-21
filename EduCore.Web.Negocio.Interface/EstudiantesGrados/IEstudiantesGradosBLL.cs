using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IEstudiantesGradosBLL
{
    TRespuesta<object> Consultar (EstudiantesGrados obj);
    TRespuesta<object> ConsultarGrados(ListadoUtilidades obj);
    TRespuesta<object> ConsultarEstudiantes(ListadoUtilidades obj);
    TRespuesta<object> Insertar(EstudiantesGradosDTO obj);
    TRespuesta<object> Actualizar(EstudiantesGradosDTO obj);
    TRespuesta<object> Eliminar(EstudiantesGrados obj);
}