
namespace Services.DTOs
{
    public class ContributionDto
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }

        public decimal Amount { get; set; }

        public DateTime ContributionDate { get; set; }
    }
}
