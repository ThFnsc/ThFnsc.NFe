using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.SMTP
{
    public class EditSMTPModel
    {
        public int Id { get; set; }

        [Display(Name = "Host")]
        [Required]
        public string Host { get; set; }

        [Display(Name = "Porta")]
        [Required]
        [Range(1, ushort.MaxValue)]
        public int Port { get; set; }

        [Display(Name = "Usar criptografia")]
        [Required]
        public bool UseEncryption { get; set; }

        [Display(Name = "Conta")]
        [EmailAddress]
        [Required]
        public string Account { get; set; }

        [Display(Name = "Usuário")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string AccountName { get; set; }
    }
}
