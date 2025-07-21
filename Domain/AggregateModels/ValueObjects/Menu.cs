namespace Domain.AggregateModels
{
    using System;
    using System.Collections.Generic;
    using Domain.AggregateModels.Metadata;
    using Domain.AggregateModels.ValueObjects;

    /// <summary>
    /// This class represents the Implementation of the aggregate domain model for the Entity (Menu)
    /// </summary>
    public partial class Menu : Domain.Common.EntityBase
    {
        public Menu() { }        
        public Menu(int id, string path, string title, string icon, string className, string badge, string badgeClass,
            bool isExternalLink, bool isParent, int? menuId)
        {
            Id = id;
            Path = path;
            Title = NombreValido.Create(title, MenuMetadata.Title).Value;
            Icon = icon;
            Class = className;
            Badge = badge;
            BadgeClass = badgeClass;
            IsExternalLink = isExternalLink;
            IsParent = isParent;
            MenuId = menuId;
        }
#nullable enable
        public string? Path { get; set; }
        public NombreValido Title { get; set; }
        public string? Icon { get; set; }
        public string? Class { get; set; }
        public string? Badge { get; set; }
        public string? BadgeClass { get; set; }
        public bool IsExternalLink { get; set; }
        public bool IsParent { get; set; }
        public virtual List<Menu> SubMenu { get; set; } = null!;
        public int? MenuId { get; set; }
        //public virtual List<Role> Roles { get; set; } = null!;
    }
}
