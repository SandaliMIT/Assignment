using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public String Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string PhotoFileName { get; set; }
    }
}
