using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
namespace EduCore.Web.Negocio.Interfaces;
public interface IMedicosBLL
{
    TRespuesta<object> Consultar(Medicos objInsumo);
    TRespuesta<object> Insertar(MedicosDTO objInsumo);
    TRespuesta<object> Actualizar(MedicosDTO objInsumo);
    TRespuesta<object> Eliminar(Medicos objInsumo);
}