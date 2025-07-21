namespace Domain.AggregateModels.Metadata
{
    using Domain.Common;

    /// <summary>
    /// This class represents the Implementation metadata for the Entity (Menu)
    /// </summary>
    public static class MenuMetadata 
    {
        public static Metadata Path => new(nameof(Menu.Path), nameof(Menu.Path), 100);
        public static Metadata Title => new(nameof(Menu.Title), nameof(Menu.Title), 100);
        public static Metadata Icon => new(nameof(Menu.Icon), nameof(Menu.Icon), 100);
        public static Metadata Class => new(nameof(Menu.Class), nameof(Menu.Class), 100);
        public static Metadata Badge => new(nameof(Menu.Badge), nameof(Menu.Badge), 100);
        public static Metadata BadgeClass => new(nameof(Menu.BadgeClass), nameof(Menu.BadgeClass), 100);
        public static Metadata IsExternalLink => new(nameof(Menu.IsExternalLink), nameof(Menu.IsExternalLink), 100);
        public static Metadata IsParent => new(nameof(Menu.IsParent), nameof(Menu.IsParent), 100);
        public static Metadata SubMenu => new(nameof(Menu.SubMenu), nameof(Menu.SubMenu), 100);
        public static Metadata MenuId => new(nameof(Menu.MenuId), nameof(Menu.MenuId), 100);
        //public static Metadata Roles => new(nameof(Menu.Roles), nameof(Menu.Roles), 100);
    }
}
