using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.ScheduledGeneration
{
    public class EditScheduledGenerationModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"((((\d+,)+\d+|(\d+(\/|-)\d+)|\d+|\*) ?){5,7})")]
        [Display(Name = "Padrão CRON UTC")]
        public string CronPattern { get; set; }

        [Display(Name = "Prestador")]
        [Required]
        public int? ProviderId { get; set; }

        [Display(Name = "Para o documento")]
        [Required]
        public int? ToDocumentId { get; set; }

        [Display(Name = "Valor")]
        [Required]
        public float Value { get; set; }

        [Display(Name = "Alíquota")]
        [Required]
        public float AliquotPercentage { get; set; }

        [Display(Name = "Id do serviço")]
        [Required]
        public int ServiceId { get; set; }

        [Display(Name = "Descrição do serviço")]
        [Required]
        public string ServiceDescription { get; set; }

        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }

        [Display(Name = "Notificadores")]
        public List<int> NotifierIDs { get; set; } = new();
    }
}
