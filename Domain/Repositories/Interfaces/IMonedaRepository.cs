using Domain.AggregateModels.Moneda;
using Domain.Common;

namespace Domain.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz representa las Implementaciones De la Dao para la Entidad (Moneda)
    /// </summary>
    public partial interface IMonedaRepository :
        IRepositoryBase<Moneda> { }
}
