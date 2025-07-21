namespace Domain.AggregateModels.Metadata
{

    /// <summary>
    /// This class represents the Implementation metadata for the Entity (Entity)
    /// </summary>
    public static class EntityMetadata 
    {
        public static Domain.Common.Metadata Name => new(nameof(Entity.Name), nameof(Entity.Name), 100);
    }
}
