using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.Provider;

public class EditProviderModel
{
    public int Id { get; set; }

    [Display(Name = "Documento")]
    [Required]
    public int? DocumentId { get; set; }

    [Display(Name = "Dados")]
    public string Data { get; set; }

    [Required]
    [Display(Name = "Prefeitura")]
    public string TownHallType { get; set; }
}
