namespace Projekti_Final.Models
{
    /// <summary>
    /// Represents a single product entry inside the shopping cart.
    /// </summary>
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}
