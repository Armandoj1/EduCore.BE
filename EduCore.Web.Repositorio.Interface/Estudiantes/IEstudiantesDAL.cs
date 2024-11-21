using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IEstudiantesDAL
{
    List<Estudiantes> Consultar(Estudiantes objInsumo);
    object Insertar(EstudiantesDTO objInsumo);
    object Actualizar(EstudiantesDTO objInsumo);
    object Eliminar(Estudiantes objInsumo);
}