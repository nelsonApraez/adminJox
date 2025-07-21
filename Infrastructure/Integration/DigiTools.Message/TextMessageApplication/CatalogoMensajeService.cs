using System.Threading.Tasks;
using Domain.Services;
using Package.Utilities.Net;

namespace DigiToolsMessage.TextMessageApplication
{
    public class CatalogoMensajeService : ICatalogoMensajeService
    {
        public async Task<ResponseApi> ObtenerMensaje(int status, EnumerationMessage.Message message, string[] tags, EnumerationApplication.TypeMessage typeMessage, object obj, string resourceName)
        {
            return await Task.FromResult(TextMessageApi.BuildResponseApi(status, message, tags, typeMessage, obj, resourceName));
        }
    }
}
