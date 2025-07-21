
using Nico.Assistant.Models;

namespace Nico.Assistant.Interface
{
    public interface IProcessOperation
    {
        public Task<ResponseNico> ExecuteOperation(ActionAgentModel process); 
    }
}
