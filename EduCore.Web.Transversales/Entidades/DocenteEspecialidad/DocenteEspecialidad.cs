using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Transversales.Entidades;

public class DocenteEspecialidad
{
    public int EspecialidadID { get; set; }
    public string DocenteID { get; set; }
    public string NombreCompleto { get; set; }
    public string NombreEspecialidad { get; set; }
}