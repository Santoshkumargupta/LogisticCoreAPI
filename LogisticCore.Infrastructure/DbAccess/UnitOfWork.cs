using LogisticCore.Infrastructure.Context;
using LogisticCore.Infrastructure.Interface;
using LogisticCore.Infrastructure.Repository;
using LogisticCore.Infrastructure.Respository;
using Microsoft.EntityFrameworkCore.Storage;

namespace LogisticCore.Infrastructure.DbAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogisticCoreContext _dbContext;
        private IUserRepository userRepository;
        private IRefreshTokenRepository refreshTokenRepository;
        private ISupplierRepository supplierRepository;
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        public UnitOfWork(LogisticCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {

            get
            {
                return userRepository = userRepository ?? new UserRepository(_dbContext);
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {

            get
            {
                return refreshTokenRepository = refreshTokenRepository ?? new RefreshTokenRepository(_dbContext);
            }
        }

        public ISupplierRepository  SupplierRepository
        {

            get
            {
                return supplierRepository = supplierRepository ?? new SupplierRepository(_dbContext);
            }
        }

        public IProductRepository ProductRepository
        {

            get
            {
                return productRepository = productRepository ?? new ProductRepositroy(_dbContext);
            }
        }

        public IOrderRepository OrderRepository
        {

            get
            {
                return  orderRepository = orderRepository ?? new OrderRepository(_dbContext);
            }
        }

        public IOrderDetailRepository OrderDetailRepository
        {

            get
            {
                return orderDetailRepository = orderDetailRepository ?? new OrderDetailRepository(_dbContext);
            }
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }
        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
