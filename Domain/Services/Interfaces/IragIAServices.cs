using System.Threading.Tasks;
using Domain.AggregateModels;

namespace Domain.Services.Interfaces
{
    public interface IragIAServices
    {
        Task<string> GetMessageRagAsync(string texto, Prompt promp);
    }
}
