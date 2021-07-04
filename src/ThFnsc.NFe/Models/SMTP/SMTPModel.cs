using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Models.SMTP
{
    public class SMTPModel
    {
        public int Id { get; set; }

        [Display(Name = "Host")]
        public string Host { get; set; }

        [Display(Name = "Porta")]
        public int Port { get; set; }

        [Display(Name = "Usar criptografia")]
        public bool UseEncryption { get; set; }

        [Display(Name = "Conta")]
        public string Account { get; set; }

        [Display(Name = "Usuário")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Nome")]
        public string AccountName { get; set; }
    }
}
