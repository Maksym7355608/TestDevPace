using TestDevPace.Data.EF;
using TestDevPace.Data.Entities;
using TestDevPace.Data.Interfaces;

namespace TestDevPace.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestDevPaceContext context;

        private CustomerRepository customerRepository;

        public IRepository<Customer> CustomerRepository => customerRepository ??= new CustomerRepository(context);

        public UnitOfWork(TestDevPaceContext db)
        {
            context = db;
        }

        public async Task SaveAsync() => await context
            .SaveChangesAsync();
        public void Save() => context
            .SaveChanges();


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
