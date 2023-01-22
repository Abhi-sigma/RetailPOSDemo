using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NuGet.Protocol;
using RetailPOS.Models;

namespace RetailPOS.Pages.Sales.Offline
{


  
    public class CreateModel : PageModel
    {
        private readonly RetailPOS.Models.RetailDbContext _context;

        public CreateModel(RetailPOS.Models.RetailDbContext context)
        {
            _context = context;
        }

        public List<Product> ProductList { get; set; }

        public IActionResult OnGet()

        {
            if (_context.Products != null)
            {
                ProductList = _context.Products.ToList();
            }
            
            return Page();
        }

        public class JsonFormatterProduct
        {
            public int Id;
            public string Name;
            public decimal Price;
            public string Image;
            public string StockUnit;
                
        }

        public JsonResult OnPostGetProducts(string hint)

        {
            string ProductJson =  "";
            List<JsonFormatterProduct> jsonFormatterProducts = new List<JsonFormatterProduct>();
            if (_context.Products != null)
            {
                ProductList = _context.Products.Include(x=>x.StockMeasuredType).Where(x=>x.Name.Contains(hint)).ToList();
                foreach(var item in ProductList)
                {
                   var product=  new JsonFormatterProduct { 
                       Id = item.Id,
                       Name = item.Name,
                       Price= item.Price,
                       Image = "/images/" + item.Filepath,
                       StockUnit = _context.StockMeasureType.FirstOrDefault(x => x.Id == item.StockMeasuredType.Id).Type
                   };
                    jsonFormatterProducts.Add(product);
                }

                        
                }
                ProductJson = JsonConvert.SerializeObject(jsonFormatterProducts);

            

            return new JsonResult(ProductJson);
            
        }

        public class JsonFormatterProductSales
        {
            public IList<SaleItems> Items;
            public decimal TotalCost;
            public DateTime CompletedOn;
               
        }

        public class SaleItems
        {
            public int Id;
            public string Name;
            public int Qty;
            public decimal Cost;
            public decimal Price;
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostSaveSalesAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Request.HasFormContentType)
            {
                IFormCollection form;
               
                                             // Or
                form = await Request.ReadFormAsync(); // async
                string data = form["data"];
                //string completedOn = form["completedOn"];
                JsonFormatterProductSales dataObj = JsonConvert.DeserializeObject<JsonFormatterProductSales>(data);
                ICollection<LineSaleItems> LineSaleItemsCollection = new List<LineSaleItems>();
                foreach (var item in dataObj.Items)
                {
                    var LineSaleItem = new LineSaleItems
                    {
                        Product = _context.Products.FirstOrDefault(x => x.Id == item.Id),
                        Quantity = item.Qty

                    };
                    LineSaleItemsCollection.Add(LineSaleItem);
                }


                OfflineSales dataToSave = new OfflineSales
                {
                    PurchaseCompletedOn = dataObj.CompletedOn,
                    TotalCost = dataObj.TotalCost,
                    LineSaleItem = LineSaleItemsCollection


                };
                _context.OfflineSales.Add(dataToSave);
                await _context.SaveChangesAsync();


            }

          

            return RedirectToPage("./Index");
        }



    }
}
