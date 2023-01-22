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
    public class IndexModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public IndexModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        public IList<OfflineSales> OfflineSales { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            
            
            if (_context.OfflineSales != null)
            {
                OfflineSales = await _context.OfflineSales.ToListAsync();
            }
        }
    }
}
