using ThFnsc.NFe.Models.Address;

namespace ThFnsc.NFe.Models.Document
{
    public class DocumentModel
    {
        public int Id { get; set; }

        public string DocType { get; set; }

        public string DocIdentifier { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public AddressModel Address { get; set; }
    }
}
