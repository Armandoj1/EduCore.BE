using Dapper;
using EduCore.Web.Data;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Base;
using EduCore.Web.Transversales.Constantes.General.Enum;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EduCore.Web.Transversales;

namespace EduCore.Web.Repositorio;


public class MedicosDAL : IMedicosDAL
{
    private string connectionString;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    public MedicosDAL()
    {
        var objConfig = new Config();
        connectionString = objConfig.GetConnectionString(Configuracion.CADENA_CONEXION_TITAN);
    }

    public List<Medicos> Consultar(Medicos obj)
    {
        try
        {
            List<Medicos> res;
            using DapperManager<Medicos> dapper = new SqlConnectionFactory<Medicos>(connectionString).GetConnectionManager();
            dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Consulta);
            dapper.AddParameter("intIdMedico", obj.idMedico == 0 ? null : obj.idMedico);
            dapper.AddParameter("strNumeroLicencia", string.IsNullOrEmpty(obj.numeroLicencia) ? null : obj.numeroLicencia);

            res = dapper.GetList(ProcedimientosAlmacenados.CRUD_MEDICOS).ToList();
            return res;
        }
        catch (Exception ex)
        {
            string msg = $"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.MEDICOS} DAL: ";
            log.Error(msg + ex.Message, ex);
            return new List<Medicos> { new() { idMedico = 0, nombreCompleto = msg + ex.Message } };
        }
    }

    public object Insertar(MedicosDTO obj)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("intOpcion", (int)EnumTipoProceso.Insertar);
                parameters.Add("intIdMedico", obj.idMedico == 0 ? null : obj.idMedico);
                parameters.Add("StrNombreCompleto", string.IsNullOrEmpty(obj.nombreCompleto) ? null : obj.nombreCompleto);
                parameters.Add("StrNumeroLicencia", string.IsNullOrEmpty(obj.numeroLicencia) ? null : obj.numeroLicencia);
                parameters.Add("strTelefono", string.IsNullOrEmpty(obj.telefono) ? null : obj.telefono);
                parameters.Add("strCorreo", string.IsNullOrEmpty(obj.correo) ? null : obj.correo);
                parameters.Add("strDireccion", string.IsNullOrEmpty(obj.direccion) ? null : obj.direccion);

                var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_MEDICOS, parameters, commandType: CommandType.StoredProcedure);

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
            string msg = $"{Mensajes.ERROR_INSERTANDO} {Funcionalidades.MEDICOS} DAL: ";
            log.Error(msg + ex.Message, ex);
            return new { filas = 0, exitoso = false, error = msg + ex.Message };
        }
    }
    public object Actualizar(MedicosDTO obj)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("intOpcion", (int)EnumTipoProceso.Actualizar);
                parameters.Add("intIdMedico", obj.idMedico == 0 ? null : obj.idMedico);
                parameters.Add("strNombreCompleto", string.IsNullOrEmpty(obj.nombreCompleto) ? null : obj.nombreCompleto);
                parameters.Add("strNumeroLicencia", string.IsNullOrEmpty(obj.numeroLicencia) ? null : obj.numeroLicencia);
                parameters.Add("strTelefono", string.IsNullOrEmpty(obj.telefono) ? null : obj.telefono);
                parameters.Add("strCorreo", string.IsNullOrEmpty(obj.correo) ? null : obj.correo);
                parameters.Add("strDireccion", string.IsNullOrEmpty(obj.direccion) ? null : obj.direccion);

                var result = connection.QueryFirstOrDefault(ProcedimientosAlmacenados.CRUD_MEDICOS, parameters, commandType: CommandType.StoredProcedure);

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
            string msg = $"{Mensajes.ERROR_ACTUALIZANDO} {Funcionalidades.MEDICOS} DAL: ";
            log.Error(msg + ex.Message, ex);
            return new { filas = 0, exitoso = false, error = msg + ex.Message };
        }
    }

    public object Eliminar(Medicos obj)
    {
        try
        {
            int res;
            using (DapperManager<Medicos> dapper = new SqlConnectionFactory<Medicos>(connectionString).GetConnectionManager())
            {
                dapper.AddParameter("intOpcion", (int)EnumTipoProceso.Eliminar);
                dapper.AddParameter("intIdMedico", obj.idMedico == 0 ? null : obj.idMedico);

                res = dapper.Execute(ProcedimientosAlmacenados.CRUD_MEDICOS);
            }

            return new { filas = res, exitoso = true, error = string.Empty };
        }
        catch (Exception ex)
        {
            string msg = $"{Mensajes.ERROR_ELIMINANDO} {Funcionalidades.MEDICOS} DAL: ";
            log.Error(msg + ex.Message, ex);
            return new { filas = 0, exitoso = false, error = msg + ex.Message };
        }
    }
}
