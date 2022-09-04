using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevPace.Business.Models;

namespace TestDevPace.Business.Interfaces
{
    public interface ICustomerService
    {
        Task CreateCustomerAsync(CustomerModel customer);
        Task UpdateCustomerAsync(CustomerModel customer);
        Task DeleteCustomerByIdAsync(int id);
        Task<CustomerModel> GetCustomerByIdAsync(int id);
        IEnumerable<CustomerModel> GetAllCustomers();
        Task<IEnumerable<CustomerModel>> SearchCustomersByParamAsync(SearchAndSort search, string text);
        Task<IEnumerable<CustomerModel>> SortCustomersByParamAsync(SearchAndSort search);
    }
}
