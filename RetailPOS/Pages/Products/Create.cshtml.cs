using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailPOS.Models;

namespace RetailPOS.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public CreateModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            if (_context.StockMeasureType != null)
            {
                StockMeasureIn = _context.StockMeasureType.ToList();
            }
            
            return Page();
        }


        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string StockUnit { get; set; }
            public IFormFile File { get; set; }
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }


        public List<StockMeasureType> StockMeasureIn { get; set; }





        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            var File = Product.File;
            if  (File!= null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "." + File.FileName.Split(".")[1];
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                   
                }
                StockMeasureType StockUnit = _context.StockMeasureType.Where(x => x.Type == Product.StockUnit).FirstOrDefault();
                Product product = new Product
                {
                    Filepath = uniqueFileName,
                    Name = Product.Name,
                    Price = Product.Price,
                    StockMeasuredType = StockUnit

                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();




            }
           

            return RedirectToPage("./Index");
        }
    }
}
