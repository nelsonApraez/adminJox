using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;
using Package.Utilities.Net;

namespace Domain.AggregateModels.ValueObjects
{
    public class EstadoRangoFechas : ValueObject
    {
        public bool Value { get; private set; } = false;

        private EstadoRangoFechas() { }

        private EstadoRangoFechas(DateTime activoDesde, DateTime activoHasta)
        {
            var fechaActual = DateTime.UtcNow;
            Value = (activoDesde < activoHasta && (activoHasta >= fechaActual) && (activoDesde <= fechaActual));
        }

        public static Result<EstadoRangoFechas, List<DomainModelExceptions>> CalcularEstado(DateTime activoDesde, DateTime activoHasta)
        {
            List<DomainModelExceptions> listExceptions = new();
            if (activoDesde > activoHasta)
            {
                listExceptions.Add(DomainExceptions.General.ValueIsInvalid("ActivoDesde", $"{EnumerationMessage.Message.RangoFechasDesde}"));
                listExceptions.Add(DomainExceptions.General.ValueIsInvalid("ActivoHasta", $"{EnumerationMessage.Message.RangoFechasHasta}"));
            }
            return new EstadoRangoFechas(activoDesde, activoHasta);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
