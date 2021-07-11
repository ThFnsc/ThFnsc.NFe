namespace ThFnsc.NFe.Services.IPMNF.Models
{
    public class DataModel
    {
        public string Password { get; set; }

        public static DataModel Example() =>
            new()
            {
                Password = "12345678"
            };
    }
}
