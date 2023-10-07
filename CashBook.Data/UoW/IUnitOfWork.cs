

namespace CashBook.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;

        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<BankTransaction> BankTransactionRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }
        IGenericRepository<Warehouse> WarehouseRepository { get; }

        Task<int> Commit();
    }
}