namespace Application.Features.Models.Dto
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the implementation for DTO pattern of the entity [Menu]
    /// </summary>
    public partial class MenuDto 
    { 
        public int Id { get; set; }

#nullable enable
        public string? Path { get; set; }

        public string? Title { get; set; }

        public string? Icon { get; set; }

        public string? Class { get; set; }

        public string? Badge { get; set; }

        public string? BadgeClass { get; set; }

        public bool IsExternalLink { get; set; }

        public bool IsParent { get; set; }
        public List<MenuDto>? SubMenu { get; set; } = null!;

        public int? MenuId { get; set; }
    }
}
