using System.ComponentModel.DataAnnotations;

namespace Projekti_Final.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
