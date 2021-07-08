namespace ThFnsc.NFe.Models.Address
{
    public class AddressModel
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public int CityId { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }
}
