using System.ComponentModel.DataAnnotations;

namespace Projekti_Final.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string AddressLine { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        /// <summary>Payment method chosen at checkout (e.g. "Cash on Delivery", "Credit Card").</summary>
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}
