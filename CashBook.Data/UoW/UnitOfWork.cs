using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CashBookDbContext _context;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<BankTransaction> _bankTransactionRepository;
        private IGenericRepository<ProductCategory> _productCategoryRepository;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Warehouse> _warehouseRepository;

        public UnitOfWork(CashBookDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new GenericRepository<User>(_context)); }
        }
        public IGenericRepository<BankTransaction> BankTransactionRepository
        {
            get { return _bankTransactionRepository ?? (_bankTransactionRepository = new GenericRepository<BankTransaction>(_context)); }
        }
        public IGenericRepository<ProductCategory> ProductCategoryRepository
        {
            get { return _productCategoryRepository ?? (_productCategoryRepository = new GenericRepository<ProductCategory>(_context)); }
        }
        public IGenericRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new GenericRepository<Product>(_context)); }
        }        
        public IGenericRepository<Warehouse> WarehouseRepository
        {
            get { return _warehouseRepository ?? (_warehouseRepository = new GenericRepository<Warehouse>(_context)); }
        }
        public async Task<int> Commit()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = await _context.SaveChangesAsync();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    _context.Dispose();
                    transaction.Rollback();

                    return -1;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
    }
}
