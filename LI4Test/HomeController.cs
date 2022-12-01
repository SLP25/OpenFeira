using Microsoft.AspNetCore.Mvc;

namespace LI4Test
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
