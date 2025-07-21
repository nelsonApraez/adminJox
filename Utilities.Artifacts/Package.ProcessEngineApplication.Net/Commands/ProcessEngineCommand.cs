using Package.ProcessEngineApplication.Net.Interfaces;
using MediatR;

namespace Package.ProcessEngineApplication.Net
{
    public record ExecuteTrace(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteNotification(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteAudit(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteLog(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteSecurityProfile(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteDataExtraction(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteUserForCompanies(IProcessRequestMessage Message) : IRequest<string>;
    public record ExecuteValidateActiveProfile(IProcessRequestMessage Message) : IRequest<string>;
}
