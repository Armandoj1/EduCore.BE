
using System.Net;

namespace EduCore.Web.Transversales.Respuesta
{
    public class TRespuesta<T>
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; } = string.Empty;
        public long? IdTransactionCode { get; set; }
        public int RowsAffected { get; set; }
        public ICollection<T> ResultadoConsulta { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
    }
}
