namespace Domain.AggregateModels.Metadata
{
    using Domain.Common;

    /// <summary>
    /// This class represents the Implementation metadata for the Entity (Role)
    /// </summary>
    public static class RoleMetadata 
    {
        public static Metadata Name => new(nameof(Role.Name), nameof(Role.Name), 100);
        //public static Metadata Menus => new(nameof(Role.Menus), nameof(Role.Menus), 100);
    }
}
