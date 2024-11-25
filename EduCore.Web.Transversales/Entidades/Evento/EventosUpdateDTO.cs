namespace EduCore.Web.Transversales.Entidades;

public class EventosUpdateDTO
{
    public int EventoID { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string UsuarioCreadorID { get; set; }
    public int TipoEventoID { get; set; }
    public int GradoID { get; set; }
}