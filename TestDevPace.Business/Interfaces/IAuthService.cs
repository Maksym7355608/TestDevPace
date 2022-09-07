using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevPace.Business.Models;

namespace TestDevPace.Business.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignInAsync(string username, string password);
        Task SignUpAsync(CustomerModel model);
        string Decrypt(string password);
    }
}
