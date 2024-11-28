using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;

namespace EduCore.Web.Negocio.Interfaces;

public interface INotasBLL
{
	TRespuesta<object> Insertar(Nota objInsumo);
	TRespuesta<object> Actualizar(Nota objInsumo);
	TRespuesta<object> ConsultarPeriodoVigente(ListadoUtilidades objInsumo);
    TRespuesta<object> VerPeriodo(VerPeriodos objInsumo);
    TRespuesta<object> HabilitarPeriodo(PeriodoVigente objInsumo);
}