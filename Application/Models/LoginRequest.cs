using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class LoginRequest
    {
        [Required]
        public string User { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
