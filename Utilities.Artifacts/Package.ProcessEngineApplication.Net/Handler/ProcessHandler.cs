namespace Package.ProcessEngineApplication.Net.Handler
{
    using Package.ProcessEngineApplication.Net.Interfaces;
    using MediatR;
    using System.Threading.Tasks;

    /// <summary>
    /// Calse del Manejador de procesos
    /// </summary>
    public class ProcessHandler : IProcessHandlerActivator
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ProcessHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo de ejecución de los handlers
        /// </summary>
        /// <param name="message">Mensaje con información para el handler</param>
        public async Task<string> ExecuteProcess(IProcessRequestMessage message)
        {
            return message.HandlerId switch
            {
                EnumHandlerId.AuditManagement => await _mediator.Send(new ExecuteAudit(message)),
                EnumHandlerId.LogHandling => await _mediator.Send(new ExecuteLog(message)),
                EnumHandlerId.Notification => await _mediator.Send(new ExecuteNotification(message)),
                EnumHandlerId.Traces => await _mediator.Send(new ExecuteTrace(message)),
                EnumHandlerId.Security => await _mediator.Send(new ExecuteSecurityProfile(message)),
                EnumHandlerId.DataExtraction => await _mediator.Send(new ExecuteDataExtraction(message)),
                EnumHandlerId.CompaniesForUser => await _mediator.Send(new ExecuteUserForCompanies(message)),
                EnumHandlerId.ValidateActiveProfile => await _mediator.Send(new ExecuteValidateActiveProfile(message)),
                _ => await _mediator.Send(new ExecuteTrace(message))
            };
        }

    }
}
