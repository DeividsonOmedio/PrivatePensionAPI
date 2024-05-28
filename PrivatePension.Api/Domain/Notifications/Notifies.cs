using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications
{
    public class Notifies
    {
        [NotMapped]
        public bool? Status { get; set; }
        [NotMapped]
        public string Message { get; set; }

        private Notifies(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public static Notifies Success(string message = "")
        {
            return new Notifies(true, message);
        }

        public static Notifies Failure(string message)
        {
            return new Notifies(false, message);
        }

    }
}
