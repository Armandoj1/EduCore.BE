using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IDocentesDAL
{
    List<Docentes> Consultar(Docentes objInsumo);
    object Eliminar(Docentes objInsumo);
    object Insertar(DocentesDTO objInsumo);
    object Actualizar(DocentesDTO objInsumo);

}
