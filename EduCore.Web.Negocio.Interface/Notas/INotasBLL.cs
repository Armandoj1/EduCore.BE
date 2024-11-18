using EduCore.Web.Transversales.Entidades.Notas;
using EduCore.Web.Transversales.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Negocio.Interfaces.Notas
{
	public interface INotasBLL
	{
		TRespuesta<object> Consultar(Nota objInsumo);
		TRespuesta<object> Insertar(NotaDTO objInsumo);
		TRespuesta<object> Actualizar(NotaDTO objInsumo);
		TRespuesta<object> Eliminar(Nota objInsumo);  // Agregar este método
	}
}