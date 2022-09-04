using TestDevPace.Data.Entities;
using TestDevPace.Data.Interfaces;

namespace TestDevPace.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
