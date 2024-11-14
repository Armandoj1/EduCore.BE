using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IDocenteEspecialidadBLL
{
    TRespuesta<Object> Consultar (DocenteEspecialidad objInsumo);
    TRespuesta<Object> Insertar(DocenteEspecialidadDTO objInsumo);
    TRespuesta<Object> Actualizar(DocenteEspecialidadDTO objInsumo);
    TRespuesta<Object> Eliminar(DocenteEspecialidadDTO objInsumo);
    TRespuesta<Object> ConsultarEspecialidad (ListadoUtilidades objInsumo);
    TRespuesta<Object> ConsultarDocente(ListadoUtilidades ObjInsumo);
}