using System.ComponentModel.DataAnnotations;

namespace Projekti_Final.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>Relative path to the uploaded image, e.g. /uploads/products/abc.jpg. Null if no image.</summary>
        public string? ImagePath { get; set; }
    }
}
