using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.NFe;

public class GenerateNFModel
{
    [Display(Name = "De")]
    public int FromId { get; set; }

    [Display(Name = "Para")]
    public int ToId { get; set; }

    [Display(Name = "Valor total")]
    public float Value { get; set; }

    [Display(Name = "Id do serviço")]
    public int ServiceId { get; set; }

    [Display(Name = "Descrição do serviço")]
    public string ServiceDescription { get; set; }

    [Display(Name = "Alíquota")]
    public float AliquotPercentage { get; set; }
}
