using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCore.Web.Transversales.Constantes.General
{
    public class EstadoPeticionesHttp
    {
        public const string OK = "OK";
        public const string BAT_REQUEST = "Error: en la peticion al servicio";
        public const string UNAUTHORIZED = "Error: fallo en la autorizacion para consumir el servicio";
        public const string FORBIDDEN = "Error: el cliente no tiene permisos para consumir el recurso";
        public const string NOT_FOUND = "Error: No se encontro el recurso en la direccion especificada";
        public const string INTERNAL_SERVER_ERROR = "Error: ocurrio un error en el servicio externo";
    }
}
