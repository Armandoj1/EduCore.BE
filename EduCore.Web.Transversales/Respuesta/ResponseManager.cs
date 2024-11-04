

using System.Collections.ObjectModel;
using System.Net;

namespace EduCore.Web.Transversales.Respuesta;

public class ResponseManager
{
    public static TRespuesta<T> ResponseError<T>(string message)
    {
        return new TRespuesta<T>
        {
            ResponseCode = HttpStatusCode.BadRequest,
            IdTransactionCode = 0,
            RowsAffected = 0,
            ResponseMessage = message,
            ResultadoConsulta = null
        };
    }
    public static Task<TRespuesta<T>> ResponseErrorAsync<T>(string message)
    {
        var response = new TRespuesta<T>
        {
            ResponseCode = HttpStatusCode.BadRequest,
            IdTransactionCode = 0,
            RowsAffected = 0,
            ResponseMessage = message,
            ResultadoConsulta = null
        };
        return Task.FromResult(response);
    }
    public static TRespuesta<T> ResponseError<T>(ICollection<T> errores)
    {
        return new TRespuesta<T>
        {
            ResponseCode = HttpStatusCode.BadRequest,
            IdTransactionCode = 0,
            RowsAffected = 0,
            ResponseMessage = string.Empty,
            ResultadoConsulta = errores
        };
    }
    public static TRespuesta<T> ResponseValidation<T>(string message)
    {
        return new TRespuesta<T>
        {
            ResponseCode = HttpStatusCode.UnprocessableEntity,
            IdTransactionCode = 0,
            RowsAffected = 0,
            ResponseMessage = message,
            ResultadoConsulta = null
        };
    }

    public static TRespuesta<T> ResponseOk<T>(int rowsAffected, ICollection<T> ResultadoConsulta, long transactionCode)
    {
        return new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = "Ok.",
            IdTransactionCode = transactionCode,
            ResultadoConsulta = ResultadoConsulta
        };
    }

    public static TRespuesta<T> ResponseOkAutoCollection<T>(int rowsAffected, T ResultadoConsulta)
    {
        return new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = "Ok.",
            ResultadoConsulta = new Collection<T> { ResultadoConsulta }
        };
    }

    public static TRespuesta<T> ResponseOk<T>(int rowsAffected, ICollection<T> ResultadoConsulta)
    {
        return new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = "Ok.",
            IdTransactionCode = null,
            ResultadoConsulta = ResultadoConsulta
        };
    }

    public static TRespuesta<T> ResponseOkMessage<T>(int rowsAffected, string message, ICollection<T> resultadoConsulta)
    {
        return new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = $"{message}",
            IdTransactionCode = null,
            ResultadoConsulta = resultadoConsulta
        };
    }

    public static Task<TRespuesta<T>> ResponseOkAsync<T>(int rowsAffected, ICollection<T> ResultadoConsulta)
    {
        var response = new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = "Ok.",
            IdTransactionCode = null,
            ResultadoConsulta = ResultadoConsulta
        };
        return Task.FromResult(response);
    }

    public static Task<TRespuesta<T>> ResponseNoContentAsync<T>(int rowsAffected, ICollection<T> ResultadoConsulta)
    {
        var response = new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.NoContent,
            ResponseMessage = string.Empty,
            ResultadoConsulta = ResultadoConsulta
        };
        return Task.FromResult(response);
    }

    public static TRespuesta<T> ResponseOkPaginacion<T>(int rowsAffected, ICollection<T> ResultadoConsulta, int totalPaginas, int totalRegistros)
    {
        return new TRespuesta<T>
        {
            RowsAffected = rowsAffected,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = "Ok.",
            IdTransactionCode = null,
            ResultadoConsulta = ResultadoConsulta,
            TotalPaginas = totalPaginas,
            TotalRegistros = totalRegistros,
        };
    }

    

    public static TRespuesta<T> ResponseStatusError<T>(string message)
    {
        return new TRespuesta<T>
        {
            RowsAffected = 0,
            ResponseCode = HttpStatusCode.OK,
            ResponseMessage = $"{message}",
            IdTransactionCode = null,
            ResultadoConsulta = null
        };
    }
}
