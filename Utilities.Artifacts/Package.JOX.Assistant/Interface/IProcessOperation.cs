
using JOX.Assistant.Models;

namespace JOX.Assistant.Interface
{
    public interface IProcessOperation
    {
        public Task<ResponseJOX> ExecuteOperation(ActionAgentModel process); 
    }
}
