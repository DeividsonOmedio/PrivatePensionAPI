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
        public bool? status { get; set; }
        [NotMapped]
        public string mensagem { get; set; } = string.Empty;
    }
}
