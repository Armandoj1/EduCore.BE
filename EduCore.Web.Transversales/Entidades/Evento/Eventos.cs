namespace EduCore.Web.Transversales.Entidades;

public class Eventos
{
    public int EventoID { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string UsuarioCredorID { get; set; }
    public string TipoEventoID { get; set; }  
    public int GradoID { get; set; }
    public string Estado { get; set; }
}