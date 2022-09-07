using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestDevPace.Business.Infrastructure.JWT;
using TestDevPace.Business.Infrastructure.Validation;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;
using TestDevPace.Data;
using TestDevPace.Data.Entities;

namespace TestDevPace.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork data;
        private readonly IMapper mapper;
        private readonly IOptions<AuthOptions> authOptions;
        public AuthService(IUnitOfWork data, IMapper mapper, IOptions<AuthOptions> authOptions)
        {
            this.data = data;
            this.mapper = mapper;
            this.authOptions = authOptions;
        }

        public async Task<string> SignInAsync(string username, string password)
        {
            var user = await FindCustomerByEmailAsync(username);
            if (user == null)
                throw new AuthorizeException("User not found", HttpStatusCode.Unauthorized);
            if (password != Decrypt(user.Password))
                throw new AuthorizeException("Password is not correct", HttpStatusCode.Unauthorized);
            else
                return GetToken(user);
        }

        private async Task<CustomerModel> FindCustomerByEmailAsync(string email)
        {
            return await Task.Run(() => mapper.Map<CustomerModel>(data.CustomerRepository.
                GetAll().
                Where(x => x.Email == email).
                FirstOrDefault()));
        }

        public async Task SignUpAsync(CustomerModel model)
        {
            if (model.IsModelEmpty())
                throw new IncorrectModelException("Model is empty");
            if (data.IsEmailExist(model))
                throw new IncorrectModelException("Email is already exist");
            if (!model.Password.IsPasswordCorrect())
                throw new IncorrectModelException("Password is short");

            var user = mapper.Map<Customer>(model);
            user.Password = Encrypt(model.Password);

            await data.CustomerRepository.AddAsync(user);
            await data.SaveAsync();
        }

        private string GetToken(CustomerModel model)
        {
            var authParams = authOptions.Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = authParams.GetSymmetricSecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(authParams.TokenLifeTime),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        private string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string password)
        {
            var data = Convert.FromBase64String(password);
            var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

    }
}
