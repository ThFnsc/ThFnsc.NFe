using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.MailTemplate
{
    public class EditMailTemplateModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Assunto")]
        [Required]
        public string Subject { get; set; }

        [Display(Name = "Corpo")]
        [Required]
        public string Body { get; set; }
    }
}
