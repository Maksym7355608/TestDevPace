using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;
using TestDevPace.Filters;

namespace TestDevPace.Controllers
{
    [ExceptionFilters]
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
