using System.ComponentModel.DataAnnotations.Schema;


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

        public static Notifies Error(string message = "")
        {
            return new Notifies(false, message);
        }


        public static Notifies Failure(string message)
        {
            return new Notifies(false, message);
        }

        public static Notifies ValidatePropertyString(string value, string nameProperty)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                return new Notifies(false, "Campo Obrigatório");
            }

            return new Notifies(true, "");
        }

        public static Notifies ValidatePropertyInt(int value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                return new Notifies(false, "Campo Obrigatório");
            }

            return new Notifies(true, "");
        }
    }
}
