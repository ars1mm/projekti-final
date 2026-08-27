using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti_Final.Data;
using Projekti_Final.Models;
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

        // GET /Cart
        public IActionResult Index()
        {
            var items = _cartService.GetItems();
            ViewBag.Total = _cartService.GetTotal();
            return View(items);
        }

        // POST /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, string returnUrl = "/Products")
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound();

            _cartService.AddItem(product);
            TempData["Message"] = $"'{product.Name}' was added to your cart.";

            return Redirect(returnUrl);
        }

        // POST /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }

        // GET /Cart/Checkout
        public IActionResult Checkout()
        {
            var items = _cartService.GetItems();
            if (!items.Any())
                return RedirectToAction(nameof(Index));

            ViewBag.Total = _cartService.GetTotal();
            ViewBag.Items = items;
            return View(new CheckoutViewModel());
        }

        // POST /Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var items = _cartService.GetItems();
            if (!items.Any())
                return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                ViewBag.Total = _cartService.GetTotal();
                ViewBag.Items = items;
                return View(model);
            }

            var order = new Order
            {
                FirstName     = model.FirstName,
                LastName      = model.LastName,
                Email         = model.Email,
                Phone         = model.Phone,
                AddressLine   = model.AddressLine,
                City          = model.City,
                PostalCode    = model.PostalCode,
                Country       = model.Country,
                PaymentMethod = model.PaymentMethod,
                PlacedAt      = DateTime.UtcNow,
                Total         = _cartService.GetTotal(),
                Items = items.Select(i => new OrderItem
                {
                    ProductId   = i.ProductId,
                    ProductName = i.Name,
                    UnitPrice   = i.Price,
                    Quantity    = i.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _cartService.Clear();

            return RedirectToAction(nameof(Confirmation), new { id = order.Id });
        }

        // GET /Cart/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }

    /// <summary>View-model for the checkout form.</summary>
    public class CheckoutViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [System.ComponentModel.DataAnnotations.Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Phone]
        [System.ComponentModel.DataAnnotations.Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(200)]
        [System.ComponentModel.DataAnnotations.Display(Name = "Address")]
        public string AddressLine { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(20)]
        [System.ComponentModel.DataAnnotations.Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Display(Name = "Country")]
        public string Country { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = "Cash on Delivery";
    }
}
