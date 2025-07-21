using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.AggregateModels.Moneda.ValueObjects
{
    public class IdentificadorMonedaValido : ValueObjectString
    {
        public IdentificadorMonedaValido() { }
        private IdentificadorMonedaValido(string valor) : base(valor)
        { }

        public static Result<IdentificadorMonedaValido, DomainModelExceptions> Create(string valor)
        {
            var longitud = 15;
            if (string.IsNullOrWhiteSpace(valor))
                return DomainExceptions.General.ValueIsRequired(nameof(Moneda.Identificador));

            var name = valor.Trim();

            if (name.Length > longitud)
                return DomainExceptions.General.InvalidLength(longitud, nameof(Moneda.Identificador));

            return new IdentificadorMonedaValido(valor);
        }
    }
}
