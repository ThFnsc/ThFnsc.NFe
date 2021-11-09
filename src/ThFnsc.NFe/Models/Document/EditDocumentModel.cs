using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThFnsc.NFe.Models.Address;

namespace ThFnsc.NFe.Models.Document
{
    public class EditDocumentModel
    {
        public static readonly List<string> SupportedDocuments = new() { "CPF", "CNPJ" };

        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string DocType { get; set; }

        [Required]
        [Display(Name = "Número")]
        public string DocIdentifier { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public EditAddressModel Address { get; set; }

        public EditDocumentModel()
        {
            Address = new();
        }
    }
}
