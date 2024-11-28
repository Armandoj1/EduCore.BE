using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;
public interface INotasDAL
{
	object Insertar(Nota obj);
	object Actualizar(Nota obj);
	List<ListadoUtilidades> ConsultarPeriodoVigente (ListadoUtilidades objInsumo);
    List<VerPeriodos> VerPeriodo(VerPeriodos objInsumo);
    object HabilitarPeriodo(PeriodoVigente periodoVigente);
}