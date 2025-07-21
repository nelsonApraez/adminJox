namespace Domain.AggregateModels.Metadata
{
    using Domain.Common;

    /// <summary>
    /// This class represents the Implementation metadata for the Entity (User)
    /// </summary>
    public static class UserMetadata 
    {
        public static Metadata Username => new(nameof(User.Username), nameof(User.Username), 100);
        public static Metadata FullName => new(nameof(User.FullName), nameof(User.FullName), 100);
        public static Metadata Email => new(nameof(User.Email), nameof(User.Email), 100);
        public static Metadata EmailConfirmed => new(nameof(User.EmailConfirmed), nameof(User.EmailConfirmed), 100);
        public static Metadata PasswordHash => new(nameof(User.PasswordHash), nameof(User.PasswordHash), 100);
        public static Metadata PhoneNumber => new(nameof(User.PhoneNumber), nameof(User.PhoneNumber), 100);
        public static Metadata RoleId => new(nameof(User.RoleId), nameof(User.RoleId), 100);
        public static Metadata Role => new(nameof(User.Role), nameof(User.Role), 100);
        public static Metadata Salt => new(nameof(User.Salt), nameof(User.Salt), 100);
    }
}
