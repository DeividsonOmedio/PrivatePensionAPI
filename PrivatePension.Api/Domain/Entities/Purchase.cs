using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }
        public User Client { get; set; } = new();

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();

        [Required]
        public DateTime PurchaseDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
