using Nico.Assistant.Models;


namespace Nico.Assistant.Interface
{
    public interface IRulesEngineServices
    {
        string DispatchWorkFlow(RequestRulesEngine request);
    }
}
