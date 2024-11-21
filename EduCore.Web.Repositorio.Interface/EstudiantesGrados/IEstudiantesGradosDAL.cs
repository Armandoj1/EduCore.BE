using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;
public interface IEstudiantesGradosDAL
{
    List<EstudiantesGrados> Consultar(EstudiantesGrados objInsumo);
    object Insertar(EstudiantesGradosDTO objInsumo);
    object Actualizar(EstudiantesGradosDTO objInsumo);
    object Eliminar(EstudiantesGrados objInsumo);
    List<ListadoUtilidades> ConsultarGrados(ListadoUtilidades objInsumo);
    List<ListadoUtilidades> ConsultarEstudiantes(ListadoUtilidades objInsumo);
}