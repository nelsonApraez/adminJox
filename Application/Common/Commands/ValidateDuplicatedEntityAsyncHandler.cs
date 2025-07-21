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
    /// Metodo para la validacion generica de reglas de Servicios de aplicacion solo para duplicados
    /// </summary>
    /// <typeparam name="DTO"></typeparam>
    /// <typeparam name="ENT"></typeparam>
    public record ValidateDuplicatedEntityAsync<DTO, ENT>(DTO ObjCurrent) : IRequest<List<ResponseApi>>
    where DTO : class, new()
    where ENT : class, new();

    public class ValidateDuplicateddEntityAsyncHandler<DTO, ENT> : BaseServiceApplication, IRequestHandler<ValidateDuplicatedEntityAsync<DTO, ENT>, List<ResponseApi>>
         where DTO : class, new()
         where ENT : class, new()
    {


        /// <summary>
        /// Componente de fluentvalidation para reglas de mapeo de datos
        /// </summary>
        protected readonly IValidator<DTO> _validator;


        public ValidateDuplicateddEntityAsyncHandler(IMediator mediator, IValidator<DTO> validator) : base(typeof(ENT).Name, mediator)
        {
            _validator = validator;
        }

        public async Task<List<ResponseApi>> Handle(ValidateDuplicatedEntityAsync<DTO, ENT> request, CancellationToken cancellationToken)
        {
            //aplica invarianza solo para duplicados

            var validateAsync = await _validator.ValidateAsync(request.ObjCurrent, options => { options.IncludeRuleSets($"{EnumerationMessage.Message.Duplicado}"); }, cancellationToken);

            AddValidation(validateAsync, request.ObjCurrent);
            return ValidationsApi;
        }
    }
}
