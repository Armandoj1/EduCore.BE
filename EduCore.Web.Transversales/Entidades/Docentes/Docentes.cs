using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Transversales.Entidades;

public class Docentes
{
    public string CC { get; set; }
    public string NombreCompleto { get; set; }
    public Date FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public int edad { get; set; }
    public string Correo { get; set; }
}