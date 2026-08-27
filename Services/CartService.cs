using System.Text.Json;
using Projekti_Final.Models;

namespace Projekti_Final.Services
{
    /// <summary>
    /// Manages the shopping cart stored in the HTTP session.
    /// </summary>
    public class CartService
    {
        private const string CartKey = "ShoppingCart";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        /// <summary>Returns all items currently in the cart.</summary>
        public List<CartItem> GetItems()
        {
            var json = Session.GetString(CartKey);
            if (string.IsNullOrEmpty(json))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        /// <summary>Adds a product to the cart, or increments quantity if already present.</summary>
        public void AddItem(Product product, int quantity = 1)
        {
            var items = GetItems();
            var existing = items.FirstOrDefault(i => i.ProductId == product.Id);

            if (existing != null)
                existing.Quantity += quantity;
            else
                items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });

            Save(items);
        }

        /// <summary>Removes a product from the cart by its ID.</summary>
        public void RemoveItem(int productId)
        {
            var items = GetItems();
            items.RemoveAll(i => i.ProductId == productId);
            Save(items);
        }

        /// <summary>Clears all items from the cart.</summary>
        public void Clear()
        {
            Session.Remove(CartKey);
        }

        /// <summary>Returns the total price of all items in the cart.</summary>
        public decimal GetTotal() => GetItems().Sum(i => i.Total);

        /// <summary>Returns the total number of individual items in the cart.</summary>
        public int GetCount() => GetItems().Sum(i => i.Quantity);

        private void Save(List<CartItem> items)
        {
            Session.SetString(CartKey, JsonSerializer.Serialize(items));
        }
    }
}
