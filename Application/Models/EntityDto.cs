namespace Application.Features.Models.Dto
{
    using System;
    /// <summary>
    /// This class represents the implementation for DTO pattern of the entity [Entity]
    /// </summary>
    public partial class EntityDto 
    { 
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
