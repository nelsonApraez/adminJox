
using System.Threading.Tasks;
using Package.Utilities.Net;

namespace Domain.Services
{
    public interface ICatalogoMensajeService
    {
        Task<ResponseApi> ObtenerMensaje(int status, EnumerationException.Message message,
                         string[] tags,
                         EnumerationApplication.TypeMessage typeMessage, object obj, string resourceName);
    }
}
