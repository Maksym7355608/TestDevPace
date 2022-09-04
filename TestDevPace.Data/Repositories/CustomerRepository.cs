using TestDevPace.Data.EF;
using TestDevPace.Data.Entities;
using TestDevPace.Data.Interfaces;

namespace TestDevPace.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly TestDevPaceContext context;

        public CustomerRepository(TestDevPaceContext context)
        {
            this.context = context;
        }

        public void Add(Customer entity) => context.Customers.Add(entity);

        public async Task AddAsync(Customer entity) => await context.Customers.AddAsync(entity);

        public void Delete(int id) => context.Customers.Remove(GetById(id));

        public async Task DeleteAsync(int id) => context.Customers.Remove(await GetByIdAsync(id));

        public Customer GetById(int id) => context.Customers.Find(id);

        public IQueryable<Customer> GetAll() => context.Customers;

        public async Task<Customer> GetByIdAsync(int id) => await context.Customers.FindAsync(id);

        public void Update(Customer entity)
        {
            var customer = GetById(entity.Id);
            context.Entry(customer).CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            var customer = await GetByIdAsync(entity.Id);
            context.Entry(customer).CurrentValues.SetValues(entity);
        }
    }
}
