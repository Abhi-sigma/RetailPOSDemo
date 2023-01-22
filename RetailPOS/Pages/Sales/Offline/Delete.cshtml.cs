using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Models;

namespace RetailPOS.Pages.Sales.Offline
{
    public class DeleteModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public DeleteModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OfflineSales OfflineSales { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OfflineSales == null)
            {
                return NotFound();
            }

            var offlinesales = await _context.OfflineSales.FirstOrDefaultAsync(m => m.Id == id);

            if (offlinesales == null)
            {
                return NotFound();
            }
            else 
            {
                OfflineSales = offlinesales;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OfflineSales == null)
            {
                return NotFound();
            }
            var offlinesales = await _context.OfflineSales.FindAsync(id);

            if (offlinesales != null)
            {
                OfflineSales = offlinesales;
                _context.OfflineSales.Remove(OfflineSales);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
