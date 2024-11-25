using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Transversales.Constantes;
using EduCore.Web.Transversales.Entidades;
using EduCore.Web.Transversales.Respuesta;
using log4net;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EduCore.Web.Negocio 
{
    public class ConsultarNotasBLL : IConsultarNotasBLL
    {
        private readonly IConsultarNotasDAL _consultarNotasDAL;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ConsultarNotasBLL(IConsultarNotasDAL consultarNotasDAL) => _consultarNotasDAL = consultarNotasDAL;

        public TRespuesta<object> ConsultarEstudiantesNotas(ConsultarNotas objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
               List<ConsultarNotas> res = _consultarNotasDAL.ConsultarEstudiantesNotas(objInsumo);
                if (res != null)
                {
                    var consultarNotas = (from r in res
                                            select new
                                            {
                                                r.NombreMateria,
                                                r.Periodo1,    
                                                r.Periodo2,
                                                r.Periodo3,
                                                r.Periodo4
                                            }).ToList();

                    resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
                }

                return ResponseManager.ResponseOk(resCollection.Count, resCollection);


            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarGradosNotas(ConsultarNotas objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ConsultarNotas> res = _consultarNotasDAL.ConsultarGradosNotas(objInsumo);
                if (res != null)
                {
                    var consultarNotas = (from r in res
                                          select new
                                          {
                                              r.NombreEstudiante,
                                              r.Nota1,
                                              r.Nota2,
                                              r.Nota3,
                                              r.Nota4
                                          }).ToList();
                    resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarGradosDocentes(ListadoUtilidades objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _consultarNotasDAL.ConsultarGradosDocentes(objInsumo);
                if (res != null)
                {
                    var consultarNotas = (from r in res
                                          select new
                                          {
                                              r.GradoID,
                                              r.NombreGrado
                                          }).ToList();
                    resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }

        public TRespuesta<object> ConsultarMateriasDocentes(ListadoUtilidades objInsumo)
        {
            Collection<object> resCollection = new Collection<object>();
            try
            {
                List<ListadoUtilidades> res = _consultarNotasDAL.ConsultarMateriasDocentes(objInsumo);
                if (res != null)
                {
                    var consultarNotas = (from r in res
                                          select new
                                          {
                                              r.MateriaID,
                                              r.NombreMateria
                                          }).ToList();
                    resCollection = new Collection<object>(consultarNotas.Cast<object>().ToList());
                }
                return ResponseManager.ResponseOk(resCollection.Count, resCollection);
            }
            catch (Exception ex)
            {
                log.Error($"{Mensajes.ERROR_CONSULTANDO} {Funcionalidades.CONSULTAR_NOTAS} BLL: {ex.Message}", ex);
                return ResponseManager.ResponseError<object>(Mensajes.ERROR_CONSULTANDO);
            }
        }
    }
}