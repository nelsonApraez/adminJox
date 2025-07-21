namespace Application.Features.Models.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This class represents the implementation for DTO pattern of the login In
    /// </summary>
    public partial class LoginInDto 
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
