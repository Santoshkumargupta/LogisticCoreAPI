using LogisticCore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.Interface
{
    public interface ISupplierService
    {
        List<Supplier> GetAll();
        Supplier GetById(int Id);
        string AddAndUpdateSupplier(Supplier supplier);
        string DeleteSupplier(int Id);
    }
}
