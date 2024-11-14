using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;

namespace EduCore.Web.Negocio.Interfaces;

public interface IDocentesBLL
{
    TRespuesta<object> Consultar(Docentes objInsumo);
    TRespuesta<object> Consultar(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarEspecialidad(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarGrado(ListadoUtilidades objInsumo);
    TRespuesta<object> Insertar (DocentesDTO objInsumo);
    TRespuesta<object> Actualizar (DocentesDTO objInsumo);
    TRespuesta<object> Eliminar(Docentes objInsumo);
}