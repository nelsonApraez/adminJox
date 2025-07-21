namespace Domain.AggregateModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;
    using Domain.AggregateModels.Metadata;
    using Domain.AggregateModels.ValueObjects;

    /// <summary>
    /// This class represents the Implementation of the aggregate domain model for the Entity (User)
    /// </summary>
    public partial class User : Domain.Common.EntityBase
    { 
        public User() { }

        public User(int id, string username, string fullName, string email, bool emailConfirmed,
            string passwordHash, string phoneNumber, int? roleId, string? salt )
        {
            Id = id;
            Username = username;
            FullName = fullName != null ? NombreValido.Create(fullName, UserMetadata.FullName).Value : null;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
            Salt = salt;
        }

        public string Username { get; set; } = null!;

        public NombreValido FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; } = false!;

        public string PasswordHash { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        [ForeignKey("Role")]
        public int? RoleId { get; set; }
#nullable enable
        [JsonIgnore]
        public virtual Role? Role { get; set; } = null!;

        public string? Salt { get; set; } = null!;

    }
}
