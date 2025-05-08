using LogisticCore.Application.Interface;
using LogisticCore.Application.Service;
using LogisticCore.Infrastructure.DbAccess;
using LogisticCore.Infrastructure.Interface;
using LogisticCore.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticCore.Application.CustomServices
{
    public class RegistrationService
    {
        public static void Registrations(IServiceCollection service)
        {


            #region Service dependency
            service.AddTransient<ISupplierService, SupplierService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<ITokenService, TokenService>();

            #endregion


            #region Repository dependency
            //service.AddTransient<IRepository,GenericRepository>();

            service.AddScoped<IUserRepository, UserRepository>();
            #endregion

        }
    }
}
