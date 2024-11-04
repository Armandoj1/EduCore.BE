namespace EduCore.Web.Transversales.Entidades;

public class Medicos
{
    public int idMedico { get; set; }
    public string nombreCompleto { get; set; }
    public string numeroLicencia { get; set; }
    public string telefono { get; set; }
    public string correo { get; set; }
    public string direccion { get; set; }
    public DateTime fechaRegistro { get; set; }
    public DateTime fechaModificacion { get; set; }
    public string usuarioRegistro { get; set; }
}
