using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Pages.NF;

public class XMLModel : PageModel
{
    private readonly NFContext _context;

    public XMLModel(NFContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> OnGetAsync(int id)
    {
        var nf = await _context.NFes
            .OfId(id)
            .Select(nf => new
            {
                Series = nf.Series,
                XML = nf.ReturnedXMLContent
            })
            .SingleAsync();
        return File(Encoding.UTF8.GetBytes(nf.XML), "application/xml", $"NF-{nf.Series}.xml");
    }
}
