using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IDocentesDAL
{
    List<Docentes> Consultar(Docentes objInsumo);
    List <ListadoUtilidades> Consultar(ListadoUtilidades objInsumo);
    List <ListadoUtilidades> CosultarEspecialidad(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarGrado(ListadoUtilidades objInsumo);
    object Eliminar(Docentes objInsumo);
    object Insertar(DocentesDTO objInsumo);
    object Actualizar(DocentesDTO objInsumo);
}