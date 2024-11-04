using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
namespace EduCore.Web.Transversales.Entidades;

public class DocentesDTO
{
    public string CC { get; set; }
    public string NombreCompleto { get; set; }
    public Date FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
}
