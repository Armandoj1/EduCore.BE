using System.Text.Json.Serialization;

namespace EduCore.Web.Transversales.Entidades;

public class Estudiantes
{
    public string CC { get; set; }
    public string NombreCompleto { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public int edad { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    
    [JsonIgnore]
    public int GradoID { get; set; }

    [JsonIgnore]
    public string NombreGrado { get; set; }

}