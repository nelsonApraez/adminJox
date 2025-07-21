namespace Application.Features.Models.Dto
{
    using System;

    /// <summary>
    /// This class represents the implementation for DTO pattern of the entity [AuthorizationPermissions]
    /// </summary>
    public partial class AuthorizationPermissionsDto
    { 
        public int Id { get; set; }

        public int EntityId { get; set; }

        public int RoleId { get; set; }

        public bool PermissionCreate { get; set; }

        public bool PermissionUpdate { get; set; }

        public bool PermissionDelete { get; set; }

        public bool PermissionView { get; set; }

        public bool PermissionList { get; set; }
#nullable enable
        public string? EntityName { get; set; }

        public string? RoleName { get; set; }
    }
}
