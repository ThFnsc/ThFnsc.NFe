using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Pages.NF;

public class PDFModel : PageModel
{
    private readonly NFContext _context;

    public PDFModel(NFContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> OnGetAsync(int id)
    {
        var nf = await _context.NFes
            .OfId(id)
            .Select(nf => new
            {
                nf.Series,
                PDF = nf.ReturnedPDF
            })
            .SingleAsync();
        return File(nf.PDF, "application/pdf", $"NF-{nf.Series}.pdf");
    }
}
