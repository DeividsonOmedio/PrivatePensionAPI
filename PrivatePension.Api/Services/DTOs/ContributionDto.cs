
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs
{
    public class ContributionDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ContributionDate { get; set; }
    }
}
