using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Context;
using LogisticCore.Infrastructure.DbAccess;
using LogisticCore.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Infrastructure.Respository
{
    public class OrderRepository :GenericRepository<Order>, IOrderRepository
    {
        private readonly LogisticCoreContext _dbContext;
        public OrderRepository(LogisticCoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
