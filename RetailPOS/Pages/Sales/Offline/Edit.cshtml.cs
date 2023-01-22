using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Models;

namespace RetailPOS.Pages.Sales.Offline
{
    public class EditModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public EditModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfflineSales OfflineSales { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OfflineSales == null)
            {
                return NotFound();
            }

            var offlinesales =  await _context.OfflineSales.FirstOrDefaultAsync(m => m.Id == id);
            if (offlinesales == null)
            {
                return NotFound();
            }
            OfflineSales = offlinesales;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OfflineSales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfflineSalesExists(OfflineSales.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OfflineSalesExists(int id)
        {
          return _context.OfflineSales.Any(e => e.Id == id);
        }
    }
}
