#nullable disable

using System;
using Domain.AggregateModels.Moneda.Specs;
using Domain.AggregateModels.Moneda.ValueObjects;
using Domain.AggregateModels.ValueObjects;

namespace Domain.AggregateModels.Moneda
{
    public partial class Moneda : Domain.Common.Entity
    {
        public Moneda()
        {

        }

        public Moneda(string nombreMoneda, string identificador,
                            string descripcion, RangoFechas rangoFechas)
        {
            Identificador = IdentificadorMonedaValido.Create(identificador).Value;
            Nombre = NombreValido.Create(nombreMoneda, MonedaMetadata.Nombre).Value;
            Descripcion = NombreValido.Create(descripcion, MonedaMetadata.Descripcion).Value;
            RangoFechas = rangoFechas;
        }

        public int Codigo { get; set; }
        public bool Estado { get => EstadoRangoFechas.CalcularEstado(ActivoDesde, ActivoHasta).Value.Value; }
        public IdentificadorMonedaValido Identificador { get; private set; }
        public NombreValido Nombre { get; private set; }
        public NombreValido Descripcion { get; private set; }
        public RangoFechas RangoFechas { get => RangoFechas.Create(ActivoDesde, ActivoHasta).Value; private set { ActivoDesde = value.ActivoDesde; ActivoHasta = value.ActivoHasta; } }
        public DateTime ActivoDesde { get; private set; }
        public DateTime ActivoHasta { get; private set; }
    }
}
