using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Domain.Dtos
{
    public class UserDtoForCreation
    {
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 6)]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
