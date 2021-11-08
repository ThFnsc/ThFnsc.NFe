namespace ThFnsc.NFe.Services.ContaJa.Notifier;

public class ContaJaNotifierModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string RazorFilename { get; set; } = "NF-@(Model.Series).xml";

    public string NFDescription { get; set; } = "Prestação de serviços";
}
