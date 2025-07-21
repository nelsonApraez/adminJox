namespace Domain.AggregateModels
{
    using Domain.AggregateModels.Metadata;
    using Domain.AggregateModels.ValueObjects;

    /// <summary>
    /// This class represents the Implementation of the aggregate domain model for the Entity (Audit)
    /// </summary>
    public partial class Entity : Domain.Common.EntityBase
    { 
        public Entity() { }

        public Entity(int id, string name)
        {
            Id = id;
            Name = NombreValido.Create(name,EntityMetadata.Name).Value;
        }

        public NombreValido Name { get; set; } = null!;

    }
}
