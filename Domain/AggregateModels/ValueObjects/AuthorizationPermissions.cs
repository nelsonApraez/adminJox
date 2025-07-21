namespace Domain.AggregateModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    /// <summary>
    /// This class represents the Implementation of the aggregate domain model for the Entity (AuthorizationPermissions)
    /// </summary>
    public partial class AuthorizationPermissions : Domain.Common.EntityBase
    {
        public  AuthorizationPermissions() { }
        public AuthorizationPermissions(int id, int entityId, int roleId, bool permissionCreate, 
            bool permissionUpdate, bool permissionDelete, bool permissionView, bool permissionList)
        {
            Id = id;
            EntityId = entityId;
            RoleId = roleId;
            PermissionCreate = permissionCreate;
            PermissionUpdate = permissionUpdate;
            PermissionDelete = permissionDelete;
            PermissionView = permissionView;
            PermissionList = permissionList;
        }

        [ForeignKey("Entity")]
        public int EntityId { get; set; }

        [JsonIgnore]
        public virtual Entity Entity { get; set; } = null!;

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;

        public bool PermissionCreate { get; set; }

        public bool PermissionUpdate { get; set; }

        public bool PermissionDelete { get; set; }

        public bool PermissionView { get; set; }

        public bool PermissionList { get; set; }
    }
}
