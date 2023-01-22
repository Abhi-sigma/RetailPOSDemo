using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailPOS.Models;

namespace RetailPOS.Pages.Products
{
    public class CreateStockUnitModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public CreateStockUnitModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StockMeasureType StockMeasureType { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StockMeasureType.Add(StockMeasureType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
