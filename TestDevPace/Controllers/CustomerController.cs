using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;
using TestDevPace.Filters;

namespace TestDevPace.Controllers
{
    [Controller]
    [ExceptionFilters]
    public class CustomerController : Controller
    {
        private ICustomerService customerService;
        private IAuthService authService;
        private int userId => int.Parse(User.Claims.ElementAt(0).Value);
        public CustomerController(ICustomerService customerService, IAuthService authService)
        {
            this.customerService = customerService;
            this.authService = authService;
        }
        [HttpGet("Customer/Index")]
        public IActionResult Index()
        {
            return View(customerService.GetAllCustomers());
        }

        [HttpPost("Customer/Sorted")]
        public async Task<IActionResult> IndexAsync(SearchAndSort searchAndSort, string text)
        {
            if (text == null)
                return View(await customerService.SortCustomersByParamAsync(searchAndSort));
            else
                return View(await customerService.SearchCustomersByParamAsync(searchAndSort, text));
        }
        [HttpPost("Customer/Delete")]
        public async Task<IActionResult> DeleteAsync(int id, string password)
        {
            if (await ValidatePasswordAsync(id, password))
            {
                await customerService.DeleteCustomerByIdAsync(id);
                return RedirectToAction("Index", "Customer");
            }
            else
                return BadRequest("Incorrect password");
        }

        [HttpGet("Customer/Delete")]
        public IActionResult DeleteForm(int id)
        {
            return View(id);
        }

        [HttpGet("Customer/Update")]
        public async Task<IActionResult> UpdateFormAsync(int id)
        {
            var customer = await customerService.GetCustomerByIdAsync(id);
            return View(customer);
        }

        [HttpPost("Customer/Update")]
        public async Task<IActionResult> UpdateAsync(CustomerModel model, string oldPassword)
        {
            if (await ValidatePasswordAsync(model.Id, oldPassword))
            {
                await customerService.UpdateCustomerAsync(model);
                return RedirectToAction("Index", "Customer");
            }
            else
                return BadRequest("Incorrect password");
        }

        private async Task<bool> ValidatePasswordAsync(int id, string password)
        {
            var pass = (await customerService.GetCustomerByIdAsync(id)).Password;
            var decr = authService.Decrypt(pass);
            return password == decr;
        }
    }
}
