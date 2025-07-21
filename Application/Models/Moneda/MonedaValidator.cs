using Application.Common.Validators;
using Application.Features.Models.Dto;
using AutoMapper;
using Domain.AggregateModels.Moneda;
using Domain.AggregateModels.Moneda.Specs;
using Domain.AggregateModels.Moneda.ValueObjects;
using Domain.AggregateModels.ValueObjects;
using Domain.Repositories.Interfaces;
using FluentValidation;
using Package.Utilities.Net;

namespace Application.Models.Validators
{
    public class MonedaValidator : AbstractValidator<MonedaDto>
    {
        private readonly IMonedaRepository _repository;

        public MonedaValidator(IMonedaRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Identificador)
                .MustBeValueObject(s => IdentificadorMonedaValido.Create(s));

            RuleFor(x => x.Nombre)
                .MustBeValueObject(s => NombreValido.Create(s, MonedaMetadata.Nombre));

            RuleFor(x => x.Descripcion)
                .MustBeValueObject(s => NombreValido.Create(s, MonedaMetadata.Descripcion));

            RuleFor(x => x)
                .MustBeValueObject(s => RangoFechas.Create(s.ActivoDesde, s.ActivoHasta));

            RuleSet($"{EnumerationMessage.Message.Duplicado}", () =>
            {
                RuleFor(v => v)
                   .MustAsync(async (x, cancellation) =>
                   {
                       return await _repository.ExistElementAsync(MonedaSpecification.ExisteMonedaPorCodigo(x.Identificador, x.Codigo));
                   }).WithErrorCode($"{EnumerationMessage.Message.Duplicado}.{nameof(Moneda.Identificador)}")
                      .WithName(nameof(Moneda.Identificador));

                RuleFor(v => v)
                        .MustAsync(async (x, cancellation) =>
                        {
                            return await _repository.ExistElementAsync(MonedaSpecification.ExisteMonedaPorNombre(x.Nombre, x.Codigo));
                        }).WithErrorCode($"{EnumerationMessage.Message.Duplicado}.{nameof(Moneda.Nombre)}")
                          .WithName(nameof(Moneda.Nombre));
            });
        }
    }


    public static class MonedaMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<MonedaDto, Moneda>()
                .ConstructUsing(s => s != null ?
                   new Moneda(s.Nombre,
                   s.Identificador,
                   s.Descripcion,
                   RangoFechas.Create(s.ActivoDesde, s.ActivoHasta).Value) : null);
            cnf.CreateMap<Moneda, MonedaDto>();
        }
    }
}
