namespace Domain.AggregateModels.Moneda.Specs
{
    public static class MonedaMetadata
    {
        public static Domain.Common.Metadata Nombre => new(nameof(Moneda.Nombre), nameof(Moneda.Nombre), 250);
        public static Domain.Common.Metadata Descripcion => new(nameof(Moneda.Descripcion), nameof(Moneda.Descripcion), 4000);
    }
}
