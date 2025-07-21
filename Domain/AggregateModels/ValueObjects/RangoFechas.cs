using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Common.Enums;
using Package.Utilities.Net;

namespace Domain.AggregateModels.ValueObjects
{
    public class RangoFechas : ValueObject
    {
        public DateTime ActivoDesde { get; private set; }
        public DateTime ActivoHasta { get; private set; }

        private RangoFechas() { }

        private RangoFechas(DateTime activoDesde, DateTime activoHasta)
        {
            ActivoDesde = activoDesde;
            ActivoHasta = activoHasta;
        }

        public static Result<RangoFechas, List<DomainModelExceptions>> Create(DateTime activoDesde, DateTime activoHasta)
        {
            List<DomainModelExceptions> listExceptions = new();
            if (activoDesde > activoHasta)
            {
                listExceptions.Add(DomainExceptions.General.ValueIsInvalid(nameof(ActivoDesde), $"{EnumerationMessage.Message.RangoFechasDesde}"));
                listExceptions.Add(DomainExceptions.General.ValueIsInvalid(nameof(ActivoHasta), $"{EnumerationMessage.Message.RangoFechasHasta}"));
            }
            if (listExceptions.Count > 0) return listExceptions;

            return new RangoFechas(activoDesde, activoHasta);
        }

        public static Result<RangoFechas, List<DomainModelExceptions>> Create(DateTimeOffset? activoDesde, DateTimeOffset? activoHasta)
        {
            List<DomainModelExceptions> listExceptions = new();
            if (activoDesde == null)
                listExceptions.Add(DomainExceptions.General.ValueIsRequired(FechaActivo.DESDE, FechaActivoConEspacio.DESDE));
            if (activoHasta == null)
                listExceptions.Add(DomainExceptions.General.ValueIsRequired(FechaActivo.HASTA, FechaActivoConEspacio.HASTA));

            if (listExceptions.Count > 0) return listExceptions;

            return Create(activoDesde.Value.UtcDateTime, activoHasta.Value.UtcDateTime);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}


