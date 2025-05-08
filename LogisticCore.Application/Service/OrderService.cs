using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Order> GetAll()
        {
            var result = _unitOfWork.OrderRepository.GetAll().ToList();
            return result;

        }

        public Order GetById(int Id)
        {
            var result = _unitOfWork.OrderRepository.GetById(Id);
            return result;
        }

        public string AddAndUpdateOrder(Order model)
        {
            try
            {
                string respMessage = string.Empty;
                if (model.Id == 0)
                {
                    _unitOfWork.OrderRepository.Add(model);
                    respMessage = "Successfully Saved";
                }
                else
                {
                    _unitOfWork.OrderRepository.Update(model);
                    respMessage = "Successfully Update";
                }

                _unitOfWork.Save();
                return respMessage;

            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        }

        public string DeleteOrder(int Id)
        {
            var order = _unitOfWork.OrderRepository.GetById(Id);
            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.Save();
            return "Successfully Deleted";
        }
    }
}
