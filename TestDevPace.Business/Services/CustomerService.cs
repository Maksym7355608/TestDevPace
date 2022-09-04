using AutoMapper;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Models;
using TestDevPace.Data;
using TestDevPace.Data.Entities;
using TestDevPace.Business.Models.Enums;
using TestDevPace.Business.BusinessLogic;

namespace TestDevPace.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork data;
        private readonly IMapper mapper;

        public CustomerService(IUnitOfWork data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task CreateCustomerAsync(CustomerModel customer)
        {
            if (customer == null)
                throw new Exception("null");
            if (data.CustomerRepository.GetAll().Select(x => x.Email).Contains(customer.Email))
                throw new Exception("Email is already exist");
            if (customer.Password.Length < 8)
                throw new Exception("Password is short");

            await data.CustomerRepository.AddAsync(mapper.Map<Customer>(customer));
            await data.SaveAsync();
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            if (!data.CustomerRepository.GetAll().Select(x => x.Id).Contains(id))
                throw new Exception("Id does not exist");
            await data.CustomerRepository.DeleteAsync(id);
            await data.SaveAsync();
        }

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            return mapper.Map<IEnumerable<CustomerModel>>(data.CustomerRepository.GetAll().AsEnumerable());
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            if (!data.CustomerRepository.GetAll().Select(x => x.Id).Contains(id))
                throw new Exception("Id does not exist");
            return mapper.Map<CustomerModel>(await data.CustomerRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CustomerModel>> SearchCustomersByParamAsync(SearchAndSort search, string text)
        {
            if (search == null)
                search = new SearchAndSort
                {
                    Category = SearchingCategories.Id,
                    SortType = TypeOfSorting.Ascending
                };
            if (search.Category == null)
                search.Category = SearchingCategories.Id;
            if (search.SortType == null)
                search.SortType = TypeOfSorting.Ascending;
            if (text == null)
                throw new Exception("Empty text");

            if (search.SortType.Equals(TypeOfSorting.Ascending))
                return mapper.Map<IEnumerable<CustomerModel>>(data.CustomerRepository.GetAll())
                    .SearchWithFilters(search, text)
                    .OrderBy(model => model.SortByField(search));
            else
                return mapper.Map<IEnumerable<CustomerModel>>(data.CustomerRepository.GetAll())
                    .SearchWithFilters(search, text)
                    .OrderByDescending(model => model.SortByField(search));
        }

        public async Task<IEnumerable<CustomerModel>> SortCustomersByParamAsync(SearchAndSort search)
        {
            if (search == null)
                search = new SearchAndSort
                {
                    Category = SearchingCategories.Id,
                    SortType = TypeOfSorting.Ascending
                };
            if (search.Category == null)
                search.Category = SearchingCategories.Id;
            if (search.SortType == null)
                search.SortType = TypeOfSorting.Ascending;


            if (search.SortType.Equals(TypeOfSorting.Ascending))
                return mapper.Map<IEnumerable<CustomerModel>>(data.CustomerRepository.GetAll())
                    .OrderBy(model => model.SortByField(search));
            else
                return mapper.Map<IEnumerable<CustomerModel>>(data.CustomerRepository.GetAll())
                    .OrderByDescending(model => model.SortByField(search));
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            if (!data.CustomerRepository.GetAll().Select(x => x.Id).Contains(customer.Id))
                throw new Exception("Id does not exist");
            await data.CustomerRepository.UpdateAsync(mapper.Map<Customer>(customer));
            await data.SaveAsync();
        }
    }
}
