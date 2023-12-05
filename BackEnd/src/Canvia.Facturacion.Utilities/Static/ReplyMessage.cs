using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Facturacion.Utilities.Static
{
    public record ReplyMessage
    {
        public const string MESSAGE_QUERY = "Consulta exitosa.";
        public const string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public const string MESSAGE_SAVE = "Se registró corectamente.";
        public const string MESSAGE_UPDATE = "Se actualizo correctamente.";
        public const string MESSAGE_DELETE = "Se elimino correctamente.";
        public const string MESSAGE_EXISTS = "El registro ya existe.";
        public const string MESSAGE_ACTIVATE = "El registro ha sido activado.";
        public const string MESSAGE_TOKEN = "Token generado correctamente.";
        public const string MESSAGE_VALIDATE = "Errores de validación.";
        public const string MESSAGE_FAILED = "Operación fallida.";
    }
}
