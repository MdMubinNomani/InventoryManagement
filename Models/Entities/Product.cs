using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InventoryManagement.Models.Entities
{
    public class Product
    {
        [Key]
        public Guid PId { get; set; } 
        public string PName { get; set; }
        public string PAmount { get; set; }
        public float UnitPrice { get; set; }
        

    }
}
