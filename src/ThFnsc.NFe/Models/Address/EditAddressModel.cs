using System;
using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.Address
{
    public class EditAddressModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Logradouro")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Número")]
        public string StreetNumber { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Id cidade")]
        public int CityId { get; set; }

        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Código postal")]
        public string PostalCode { get; set; }
    }
}