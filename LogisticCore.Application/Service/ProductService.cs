using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Application.Service
{
    public class ProductService :IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Product> GetAll()
        {
            var result = _unitOfWork.ProductRepository.GetAll().ToList();
            return result;

        }

        public Product GetById(int Id)
        {
            var result = _unitOfWork.ProductRepository.GetById(Id);
            return result;
        }

        public string AddAndUpdateProduct(Product model)
        {
            try
            {
                string respMessage = string.Empty;
                model.CreatedDate = DateTime.Now;
                if (model.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(model);
                    respMessage = "Successfully Saved";
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(model);
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

        public string DeleteProduct(int Id)
        {
            var product = _unitOfWork.ProductRepository.GetById(Id);
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();
            return "Successfully Deleted";
        }
    }
}
