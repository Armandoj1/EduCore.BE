using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;

public interface IGradosMateriasBLL
{
    TRespuesta<object> Consultar (GradosMaterias obj);  
    TRespuesta<object> Insertar(GradosMateriasDTO obj);
    TRespuesta<object> Actualizar (GradosMateriasUpdateDTO obj);
    TRespuesta<object> Eliminar(GradosMaterias obj);
    TRespuesta<object> ConsultarMaterias(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarGrados(ListadoUtilidades objInsumo);
}