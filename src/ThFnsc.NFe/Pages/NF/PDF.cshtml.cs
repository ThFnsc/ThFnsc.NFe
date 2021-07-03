using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ThFnsc.NFe.Infra.Applications;

namespace ThFnsc.NFe.Pages
{
    public class PDFModel : PageModel
    {
        private readonly NFeAppService _nfe;

        public PDFModel(NFeAppService nfe)
        {
            _nfe = nfe;
        }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            var (stream, nf) = await _nfe.PDFForIdAsync(id);
            return File(stream, "application/pdf", $"NF-{nf.Series}.pdf");
        }
    }
}
