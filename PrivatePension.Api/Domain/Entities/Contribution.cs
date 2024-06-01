using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Notifications;

namespace Domain.Entities
{
    [Table("Contributions")]
    public class Contribution
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; } = new();

        public decimal Amount { get; set; }

        public DateTime ContributionDate { get; set; }
    }
}
