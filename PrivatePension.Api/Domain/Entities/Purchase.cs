using Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Purchase : Notifies
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; } = new();
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();
        public DateTime PurchaseDate { get; set; }
        public bool IsApproved { get; set; } 
    }
}
