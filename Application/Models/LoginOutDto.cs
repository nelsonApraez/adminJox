namespace Application.Features.Models.Dto
{

    /// <summary>
    /// This class represents the implementation for DTO pattern of the login result
    /// </summary>
    public partial class LoginOutDto 
    {
        public string FullName { get; set; } = null!;

        public string Token { get; set; } = null!;

        public int RoleId { get; set; } = 0!;

        public string RoleName { get; set; } = null!;
    }
}
