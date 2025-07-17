using AlgebraWebShop2025.Data;
using Microsoft.AspNetCore.Mvc;

namespace AlgebraWebShop2025.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public const string SessionKeyName = "_cart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            //TODO: davanje u cart...
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            //TODO: micanje iz carta...
            return RedirectToAction(nameof(Index));
        }
    }
}
