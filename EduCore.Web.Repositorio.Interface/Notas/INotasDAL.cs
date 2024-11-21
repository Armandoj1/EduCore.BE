using EduCore.Web.Transversales.Entidades.Notas;
namespace EduCore.Web.Repositorio.Interface;
public interface INotasDAL
{
	List<Nota> Consultar(Nota obj);
	object Eliminar(Nota obj);
	object Insertar(NotaDTO obj);
	object Actualizar(NotaDTO obj);
}