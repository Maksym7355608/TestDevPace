using Microsoft.AspNetCore.Mvc;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;

namespace TestDevPace.Controllers
{
    [Controller]
    public class CustomerController : Controller
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            return View(customerService.GetAllCustomers());
        }
    }
}
