namespace Domain.AggregateModels
{
    using System;
    using System.Collections.Generic;
    using Domain.AggregateModels.Metadata;
    using Domain.AggregateModels.ValueObjects;
    using Domain.Common;

    /// <summary>
    /// This class represents the Implementation of the aggregate domain model for the Entity (Audit)
    /// </summary>
    public partial class Role : Domain.Common.EntityBase
    { 
        public Role() { }

        public Role(int id, string name )
        {
            Id = id;
            Name = NombreValido.Create(name, RoleMetadata.Name).Value;
        }

        public NombreValido Name { get; set; } = null!;

        //public virtual List<Menu> Menus { get; set; } = null!;

    }
}
