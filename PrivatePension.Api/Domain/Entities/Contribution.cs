using System.ComponentModel.DataAnnotations;
using Domain.Notifications;

namespace Domain.Entities
{
    public class Contribution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; } = new();

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ContributionDate { get; set; }
    }
}
