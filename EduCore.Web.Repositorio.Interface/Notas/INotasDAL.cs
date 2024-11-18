using EduCore.Web.Transversales.Entidades.Notas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Repositorio.Interface.Notas
{
	public interface INotasDAL
	{
		List<Nota> Consultar(Nota obj);
		object Eliminar(Nota obj);
		object Insertar(NotaDTO obj);
		object Actualizar(NotaDTO obj);
	}
}
