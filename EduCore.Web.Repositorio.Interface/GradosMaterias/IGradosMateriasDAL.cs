using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IGradosMateriasDAL
{
    List<GradosMaterias> Consultar(GradosMaterias objInsumo);
    object Insertar(GradosMateriasDTO objInsumo);
    object Actualizar(GradosMateriasUpdateDTO objInsumo);
    object Eliminar(GradosMaterias objInsumo);
    List<ListadoUtilidades> ConsultarMaterias(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades objInsumo);
}