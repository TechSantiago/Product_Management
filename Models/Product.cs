using System.ComponentModel.DataAnnotations; 
namespace WebApplication3.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string NameP { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }    
    }
}
   

