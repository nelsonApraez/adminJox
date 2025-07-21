namespace Application.Features.Models.Dto
{
    using System;
    /// <summary>
    /// This class represents the implementation for DTO pattern of the entity [Role]
    /// </summary>
    public partial class RoleDto 
    { 
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
