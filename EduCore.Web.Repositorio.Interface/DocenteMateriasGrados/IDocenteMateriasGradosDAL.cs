using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IDocenteMateriasGradosDAL
{
    List<DocenteMateriasGradosList> Consultar(DocenteMateriasGradosList objInsumo);
    object Eliminar(DocenteMateriasGrados objInsumo);
    object Insertar(DocenteMateriasGrados objInsumo);
    object Actualizar(DocenteMateriasGrados objInsumo);
    List<ListadoUtilidades> ConsultarDocente(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarMaterias(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades objInsumo);
}