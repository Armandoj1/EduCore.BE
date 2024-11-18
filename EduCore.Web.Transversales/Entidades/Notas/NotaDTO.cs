using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Transversales.Entidades.Notas
{
	public class NotaDTO
	{
		public string EstudianteCC { get; set; }
		public string MateriaID { get; set; }
		public int PeriodoID { get; set; }
		public decimal NotaValor { get; set; }
		public string Observacion { get; set; }
	}
}
