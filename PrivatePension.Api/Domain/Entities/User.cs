using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; } = string.Empty;

        public UserRolesEnum Role { get; set; }

        public decimal? WalletBalance { get; set; }
    }
}
