using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;

namespace EduCore.Web.Negocio.Interfaces;

public interface IConsultarNotasBLL
{
    TRespuesta<object> ConsultarEstudiantesNotas (ConsultarNotas objInsumo);
    TRespuesta<object> ConsultarGradosNotas(ConsultarNotas objInsumo);
    TRespuesta<object> ConsultarGradosDocentes(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarMateriasDocentes(ListadoUtilidades objInsumoo);
}