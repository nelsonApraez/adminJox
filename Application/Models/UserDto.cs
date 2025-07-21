namespace Application.Features.Models.Dto
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the implementation for DTO pattern of the entity [User]
    /// </summary>
    public partial class UserDto 
    { 
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; } = false!;

        public string PasswordHash { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int? RoleId { get; set; }
#nullable enable
        public string? RoleName { get; set; } = null!;

        public string? Salt { get; set; } = null!;
    }
}
