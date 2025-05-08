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
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly LogisticCoreContext _dbContext;
        public SupplierRepository(LogisticCoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    

    }
}
