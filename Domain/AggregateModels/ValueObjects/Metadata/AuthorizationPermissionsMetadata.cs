namespace Domain.AggregateModels.Metadata
{
    using Domain.Common;

    /// <summary>
    /// This class represents the Implementation metadata for the Entity (AuthorizationPermissions)
    /// </summary>
    public static class AuthorizationPermissionsMetadata
    {
        public static Metadata EntityId => new(nameof(AuthorizationPermissions.EntityId), nameof(AuthorizationPermissions.EntityId), 100);
        public static Metadata Entity => new(nameof(AuthorizationPermissions.Entity), nameof(AuthorizationPermissions.Entity), 100);
        public static Metadata RoleId => new(nameof(AuthorizationPermissions.RoleId), nameof(AuthorizationPermissions.RoleId), 100);
        public static Metadata Role => new(nameof(AuthorizationPermissions.Role), nameof(AuthorizationPermissions.Role), 100);
        public static Metadata PermissionCreate => new(nameof(AuthorizationPermissions.PermissionCreate), nameof(AuthorizationPermissions.PermissionCreate), 100);
        public static Metadata PermissionUpdate => new(nameof(AuthorizationPermissions.PermissionUpdate), nameof(AuthorizationPermissions.PermissionUpdate), 100);
        public static Metadata PermissionDelete => new(nameof(AuthorizationPermissions.PermissionDelete), nameof(AuthorizationPermissions.PermissionDelete), 100);
        public static Metadata PermissionView => new(nameof(AuthorizationPermissions.PermissionView), nameof(AuthorizationPermissions.PermissionView), 100);
        public static Metadata PermissionList => new(nameof(AuthorizationPermissions.PermissionList), nameof(AuthorizationPermissions.PermissionList), 100);
    }
}
