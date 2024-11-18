using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface.Usuarios;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Entidades.Usuarios;

namespace EduCore.Web.Repositorio.Usuarios
{
	public class UsuariosDAL : IUsuariosDAL
	{
		private readonly string connectionString;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

		public UsuariosDAL()
		{
			var objConfig = new Config();
			connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
		}

		public List<Usuario> Consultar(Usuario obj)
		{
			try
			{
				List<Usuario> res;
				using (DapperManager<Usuario> dapper = new SqlConnectionFactory<Usuario>(connectionString).GetConnectionManager())
				{
					dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
					dapper.AddParameter("UsuarioID", obj.UsuarioID == 0 ? null : (object)obj.UsuarioID);
					dapper.AddParameter("Usuario", string.IsNullOrEmpty(obj.NombreUsuario) ? null : obj.NombreUsuario);

					res = dapper.GetList(ProcedimientosAlmacenados.CRUD_USUARIOS).ToList();
				}

				return res;
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.USUARIOS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new List<Usuario> { new() { NombreUsuario = "0", Contrasena = msg + ex.Message } };
			}
		}

		public object Insertar(UsuariosDTO obj)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
					parameters.Add("Usuario", string.IsNullOrEmpty(obj.Usuario) ? null : obj.Usuario);
					parameters.Add("Contrasena", string.IsNullOrEmpty(obj.Contrasena) ? null : obj.Contrasena);

					var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_USUARIOS, parameters, commandType: CommandType.StoredProcedure);

					if (result != null && (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302))
					{
						return new { filas = 0, exitoso = false, error = result.responseMessage };
					}

					int filas = result?.filas ?? 0;
					return new { filas = filas, exitoso = true, error = string.Empty };
				}
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.USUARIOS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}

		public object Actualizar(UsuariosDTO obj)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
					parameters.Add("UsuarioID", obj.UsuarioID);
					parameters.Add("Usuario", string.IsNullOrEmpty(obj.Usuario) ? null : obj.Usuario);
					parameters.Add("Contrasena", string.IsNullOrEmpty(obj.Contrasena) ? null : obj.Contrasena);

					var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_USUARIOS, parameters, commandType: CommandType.StoredProcedure);

					if (result != null && (result.responseCode == 300 || result.responseCode == 301 || result.responseCode == 302))
					{
						return new { filas = 0, exitoso = false, error = result.responseMessage };
					}

					int filas = result?.filas ?? 0;
					return new { filas = filas, exitoso = true, error = string.Empty };
				}
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.USUARIOS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}

		public object Eliminar(Usuario obj)
		{
			try
			{
				int res;
				using (DapperManager<Usuario> dapper = new SqlConnectionFactory<Usuario>(connectionString).GetConnectionManager())
				{
					dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
					dapper.AddParameter("UsuarioID", obj.UsuarioID);

					res = dapper.Execute(ProcedimientosAlmacenados.CRUD_USUARIOS);
				}

				return new { filas = res, exitoso = true, error = string.Empty };
			}
			catch (Exception ex)
			{
				string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.USUARIOS} DAL: ";
				log.Error(msg + ex.Message, ex);
				return new { filas = 0, exitoso = false, error = msg + ex.Message };
			}
		}
	}
}