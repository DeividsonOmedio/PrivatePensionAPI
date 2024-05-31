using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Login
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}