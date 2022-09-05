using Microsoft.AspNetCore.Mvc;

namespace TestDevPace.Controllers
{
    [Controller]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
