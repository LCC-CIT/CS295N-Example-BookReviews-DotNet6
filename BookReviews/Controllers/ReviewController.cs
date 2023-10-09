using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index(Review model)
        {
            return View(model);
        }

        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Review(Review model)
        {
            return View("Index", model);
        }
    }
}
