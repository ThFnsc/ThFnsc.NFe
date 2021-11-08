using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.Notifier;

public class EditNotifierModel
{
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required]
    public string Title { get; set; }

    [Display(Name = "Tipo de notificador")]
    [Required]
    public string NotifierType { get; set; }

    [Display(Name = "Dados JSON")]
    [Required]
    public string JsonData { get; set; }

    [Display(Name = "Quando automático, esperar para executar")]
    [Required]
    public TimeSpan Delay { get; set; }
}
