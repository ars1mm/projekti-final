using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti_Final.Data;
using Projekti_Final.Services;

namespace Projekti_Final.Controllers
{
    /// <summary>
    /// Handles all shopping cart operations: viewing, adding, removing items, and checkout.
    /// </summary>
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        /// <summary>Displays all items currently in the cart.</summary>
        public IActionResult Index()
        {
            var items = _cartService.GetItems();
            ViewBag.Total = _cartService.GetTotal();
            return View(items);
        }

        /// <summary>Adds a product to the cart by its ID.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, string returnUrl = "/Products")
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound();

            _cartService.AddItem(product);
            TempData["Message"] = $"'{product.Name}' u shtua në shportë!";

            return Redirect(returnUrl);
        }

        /// <summary>Removes a product from the cart by its ID.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Clears the cart and shows a confirmation message (simulates checkout).</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            _cartService.Clear();
            TempData["Message"] = "Porosia juaj u dorëzua me sukses! Faleminderit për blerjen.";
            return RedirectToAction("Index", "Home");
        }
    }
}
