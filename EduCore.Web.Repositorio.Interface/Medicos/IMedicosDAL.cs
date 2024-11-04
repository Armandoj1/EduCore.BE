using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;
public interface IMedicosDAL
{
    List<Medicos> Consultar(Medicos objInsumo);
    object Eliminar(Medicos objInsumo);
    object Insertar(MedicosDTO objInsumo);
    object Actualizar(MedicosDTO objInsumo);
}