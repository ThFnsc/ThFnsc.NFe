namespace ThFnsc.NFe.Services.SMTP.Models
{
    public class ConnectionModel
    {
        public string Host { get; set; } = "smtp.example.com";

        public int Port { get; set; } = 587;

        public string Username { get; set; } = "username or address";

        public string Password { get; set; } = "12345678";

        public bool UseEncryption { get; set; } = true;

        public string AccountName { get; set; } = "John Doe";

        public string Account { get; set; } = "nfe@example.com";
    }
}
