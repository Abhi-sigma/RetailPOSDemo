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
    public class DetailsModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public DetailsModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

      public OfflineSales OfflineSales { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OfflineSales == null)
            {
                return NotFound();
            }

            var offlinesales = await _context.OfflineSales.
                                            Include(m => m.LineSaleItem).
                                            ThenInclude(m => m.Product).FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
