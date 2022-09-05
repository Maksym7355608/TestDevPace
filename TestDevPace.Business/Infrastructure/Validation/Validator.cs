using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevPace.Business.Models;
using TestDevPace.Data;

namespace TestDevPace.Business.Infrastructure.Validation
{
    public static class Validator
    {
        public static bool IsModelEmpty<T>(this T model)
        {
            return model == null;
        }

        public static bool IsEmailExist(this IUnitOfWork data, CustomerModel customer)
        {
            return data.CustomerRepository.GetAll().Select(x => x.Email).Contains(customer.Email);
        }

        public static bool IsCustomerExist(this IUnitOfWork data, int id)
        {
            return data.CustomerRepository.GetAll().Select(x => x.Id).Contains(id);
        }

        public static bool IsPasswordCorrect(this string password)
        {
            return password != null || password.Length > 8;
        }
    }
}
