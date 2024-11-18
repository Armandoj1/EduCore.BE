using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;

namespace EduCore.Web.Negocio.Interfaces;

public interface IDocenteMateriasGradosBLL
{
    TRespuesta<object> Consultar(DocenteMateriasGradosList objInsumo);
    TRespuesta<object> Insertar(DocenteMateriasGrados objInsumo);
    TRespuesta<object> Actualizar(DocenteMateriasGrados objInsumo);
    TRespuesta<object> Eliminar(DocenteMateriasGrados objInsumo);
    TRespuesta<object> ConsultarDocente(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarMaterias(ListadoUtilidades objInsumo);
    TRespuesta<object> ConsultarGrados(ListadoUtilidades objInsumo);
}