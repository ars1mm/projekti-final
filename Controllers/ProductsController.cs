using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti_Final.Data;
using Projekti_Final.Models;

namespace Projekti_Final.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        // Allowed image MIME types
        private static readonly string[] AllowedTypes = ["image/jpeg", "image/png", "image/webp", "image/gif"];
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        /// <summary>Saves an uploaded image to wwwroot/uploads/products and returns the relative URL path.</summary>
        private async Task<string?> SaveImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            if (!AllowedTypes.Contains(file.ContentType.ToLower()))
            {
                ModelState.AddModelError("Image", "Only JPEG, PNG, WebP and GIF images are allowed.");
                return null;
            }

            if (file.Length > MaxFileSize)
            {
                ModelState.AddModelError("Image", "Image must be smaller than 5 MB.");
                return null;
            }

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "products");
            Directory.CreateDirectory(uploadsDir);

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploadsDir, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/products/{fileName}";
        }

        /// <summary>Deletes an image from disk given its relative URL path.</summary>
        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;
            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }

        // ── Actions ──────────────────────────────────────────────────────────

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? image)
        {
            // Remove ImagePath from validation — we handle it manually
            ModelState.Remove(nameof(Product.ImagePath));

            if (ModelState.IsValid)
            {
                product.ImagePath = await SaveImageAsync(image);
                if (!ModelState.IsValid) return View(product); // image validation failed

                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"'{product.Name}' was added successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? image, bool removeImage = false)
        {
            if (id != product.Id) return NotFound();
            ModelState.Remove(nameof(Product.ImagePath));

            if (ModelState.IsValid)
            {
                // Fetch the existing image path before updating
                var existing = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                string? existingImagePath = existing?.ImagePath;

                if (removeImage)
                {
                    DeleteImage(existingImagePath);
                    product.ImagePath = null;
                }
                else if (image != null && image.Length > 0)
                {
                    var newPath = await SaveImageAsync(image);
                    if (!ModelState.IsValid) return View(product);
                    DeleteImage(existingImagePath); // remove old file
                    product.ImagePath = newPath;
                }
                else
                {
                    product.ImagePath = existingImagePath; // keep existing
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"'{product.Name}' was updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                DeleteImage(product.ImagePath);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"'{product.Name}' was deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
