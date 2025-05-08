using LogisticCore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.Interface
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int Id);
        string AddAndUpdateOrder(Order model);
        string DeleteOrder(int Id);
    }
}
