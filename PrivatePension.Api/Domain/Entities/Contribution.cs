using Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contribution : Notifies
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; } = new();
        public decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
    }
}
