using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduCore.Web.Transversales.Entidades;

public class Docentes
{
    public string CC { get; set; }
    public string NombreCompleto { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    [JsonIgnore]
    public int edad { get; set; }
    [JsonIgnore]
    public string Grado { get; set; }
    [JsonIgnore]
    public string Materia { get; set; }
    [JsonIgnore]
    public string Especialidad { get; set; }
    public string Correo { get; set; }
    public int EspecialidadID { get; set; }
    public string MateriaID { get; set; }
    public int GradoID { get; set; }
}