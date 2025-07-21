using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using FluentValidation;
using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Queries
{
    /// <summary>
    /// Metodo para la validacion generica de reglas de Servicios de aplicacion
    /// </summary>
    /// <typeparam name="DTO"></typeparam>
    /// <typeparam name="ENT"></typeparam>
    public record ValidateRulesEntityAsync<DTO, ENT>(DTO ObjCurrent, EnumerationApplication.Operations Nivel) : IRequest<List<ResponseApi>>
    where DTO : class, new()
    where ENT : class, new();


    public class ValidateRulesEntityAsyncHandler<DTO, ENT> : BaseServiceApplication, IRequestHandler<ValidateRulesEntityAsync<DTO, ENT>, List<ResponseApi>>
         where DTO : class, new()
         where ENT : class, new()
    {

        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;
        /// <summary>
        /// Componente de fluentvalidation para reglas de mapeo de datos
        /// </summary>
        protected readonly IValidator<DTO> _validator;


        public ValidateRulesEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion, IMediator mediator, IValidator<DTO> validator) : base(typeof(ENT).Name, mediator)
        {
            _serviceApplication = implementacion;
            _validator = validator;
        }

        public async Task<List<ResponseApi>> Handle(ValidateRulesEntityAsync<DTO, ENT> request, CancellationToken cancellationToken)
        {
            //aplica invarianza
            if (AddValidation(await _validator.ValidateAsync(request.ObjCurrent, options => { options.IncludeAllRuleSets(); }, cancellationToken), request.ObjCurrent))
            {
                var objEntity = _serviceApplication.MapObj<DTO, ENT>(request.ObjCurrent);
                return await _serviceApplication.ValidateRules(objEntity, request.Nivel);
            }
            return ValidationsApi;
        }
    }
}
