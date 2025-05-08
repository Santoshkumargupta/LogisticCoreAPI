using Microsoft.EntityFrameworkCore.Storage;

namespace LogisticCore.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }

        void Save();
        IDbContextTransaction BeginTransaction();
        void Commit(); 
        void Rollback();
    }
}
