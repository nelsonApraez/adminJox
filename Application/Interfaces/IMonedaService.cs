using Domain.AggregateModels.Moneda;

namespace Application.Features.Interfaces
{
    /// <summary>
    /// Esta Interfaz representa las Implementaciones Del Negocio para la Entidad (Moneda)
    /// </summary>
    public partial interface IMonedaService :
        BaseApplicationHelper.IBaseApplicationHelper<Moneda>
    { }
}
