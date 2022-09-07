using Microsoft.AspNetCore.Mvc;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;

namespace TestDevPace.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            var token = await authService.SignInAsync(email, password);

            return RedirectToAction("Index", "Customer");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(CustomerModel model)
        {
            await authService.SignUpAsync(model);
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
