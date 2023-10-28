using AdvanceEshop.Data;
using AdvanceEshop.Infrastructure;
using AdvanceEshop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace AdvanceEshop.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult AddToCart(int productId)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null) 
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult UpdateCart(int productId)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult RemoveFromCart(int productId)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveItem(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
    }
}
