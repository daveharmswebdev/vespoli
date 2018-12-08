using System.ComponentModel.DataAnnotations;

namespace Vespoli.Api.Dtos
{
    public class RowerForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 20 characters.")]
        public string Password { get; set; }
    }
}
