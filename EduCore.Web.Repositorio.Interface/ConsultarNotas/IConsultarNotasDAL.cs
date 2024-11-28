using EduCore.Web.Transversales.Entidades;
namespace EduCore.Web.Repositorio.Interface;

public interface IConsultarNotasDAL
{
    public List<ConsultarNotas> ConsultarEstudiantesNotas (ConsultarNotas objInsumo);
    public List<ConsultarNotas> ConsultarGradosNotas(ConsultarNotas objInsumo);
    public List<ConsultarNotas> ConsultarGradosNotasID(ConsultarNotas objInsumo);
    public List<ListadoUtilidades> ConsultarGradosDocentes (ListadoUtilidades objInsumo);
    public List<ListadoUtilidades> ConsultarMateriasDocentes(ListadoUtilidades objInsumoo);
}