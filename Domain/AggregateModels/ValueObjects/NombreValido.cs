using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.AggregateModels.ValueObjects
{
    public class NombreValido : ValueObjectString
    {
        private static string etiqueta = "Nombre";

        private static string nombre = etiqueta;


        public NombreValido() { }

        private NombreValido(string valor) : base(valor)
        {
            Longitud = 250;
        }

        private static Result<NombreValido, DomainModelExceptions> Create(string valor, int longitud)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return DomainExceptions.General.ValueIsRequired(nombre, etiqueta);

            string name = valor?.Trim();

            if (name?.Length > longitud)
                return DomainExceptions.General.InvalidLength(longitud, nombre, etiqueta);

            return new NombreValido(valor);
        }


        public static Result<NombreValido, DomainModelExceptions> Create(string valor, Domain.Common.Metadata metadata)
        {
            etiqueta = string.IsNullOrEmpty(metadata.Etiqueta) ? etiqueta : metadata.Etiqueta;
            nombre = string.IsNullOrEmpty(metadata.NombreLogico) ? nombre : metadata.NombreLogico;
            return Create(valor, metadata.Longitud);
        }

        public static Result<NombreValido, DomainModelExceptions> CreateEqual(string valor, Domain.Common.Metadata metadata)
        {
            etiqueta = string.IsNullOrEmpty(metadata.Etiqueta) ? etiqueta : metadata.Etiqueta;
            nombre = string.IsNullOrEmpty(metadata.NombreLogico) ? nombre : metadata.NombreLogico;
            if (valor?.Trim()?.Length != metadata.Longitud)
                return DomainExceptions.General.InvalidLength(metadata.Longitud, nombre, etiqueta);
            return Create(valor, metadata.Longitud);
        }

        public static Result<NombreValido, DomainModelExceptions> CreateEmpty(string valor, Domain.Common.Metadata metadata)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                return Create(valor, metadata);
            }
            else
                return new NombreValido(valor);
        }
    }
}
