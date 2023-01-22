using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailPOS.Models
{
    public class  Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public StockMeasureType StockMeasuredType { get; set; } 
        public string Filepath { get; set; }


    }


    public class StockMeasureType
    {
        public int Id { get; set; }
        public String Type { get; set; }
       
    }

    

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DatePlaced { get; set; }
    }




    public  class OfflineSales
    {
        public int Id { get; set; }
        public DateTime PurchaseCompletedOn { get; set; }

        public decimal TotalCost { get; set; }

        public ICollection<LineSaleItems> LineSaleItem { get; set; }

    }

     public class LineSaleItems
    {

        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Inventory
    {
        public List<Product> Products { get; set; }

        public void AddProduct(Product product)
        {
            // Add code to add a new product to the inventory
        }

        public void UpdateProductQuantity(int productId, int newQuantity)
        {
            // Add code to update the quantity in stock for a product
        }
    }

    public class Payment
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        public bool ProcessPayment()
        {
            // Add code to process the payment
            return true;
        }
    }

    public class Report
    {
        public string GenerateSalesReport()
        {
            // Add code to generate a sales report
            return "";
        }

        public string GenerateInventoryReport()
        {
            // Add code to generate an inventory report
            return "";
        }
    }

    public class Discount
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsPercentage { get; set; }
        public bool AppliesToAll { get; set; }
        public List<Product> Products { get; set; }

        public decimal CalculateDiscount(decimal total)
        {
            // Add code to calculate the discount
            return 0;
        }
    }

}
