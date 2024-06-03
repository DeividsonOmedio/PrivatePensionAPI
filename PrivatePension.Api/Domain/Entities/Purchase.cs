using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Purchases")]
    public class Purchase
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int ClientId { get; set; }
        public User Client { get; set; } = new();

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();

        public DateTime PurchaseDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
