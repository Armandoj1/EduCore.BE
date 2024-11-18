using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IDocenteEspecialidadDAL
{
    List<DocenteEspecialidad> Consultar(DocenteEspecialidad objInsumo);
    object Eliminar(DocenteEspecialidadDTO objInsumo);
    object Insertar(DocenteEspecialidadDTO objInsumo);
    object Actualizar(DocenteEspecialidadDTO objInsumo);
    List<ListadoUtilidades> CosultarEspecialidad(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarDocente(ListadoUtilidades ObjInsumo);
}