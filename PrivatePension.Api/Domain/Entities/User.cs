using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRolesEnum Role { get; set; }

        public decimal? WalletBalance { get; set; }
    }
}
