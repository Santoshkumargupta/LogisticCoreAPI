using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Application.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Supplier> GetAll()
        {
            var result = _unitOfWork.SupplierRepository.GetAll().ToList();
            return result;

        }

        public Supplier GetById(int Id)
        {
            var result = _unitOfWork.SupplierRepository.GetById(Id);
            return result;
        }

        public string AddAndUpdateSupplier(Supplier supplier)
        {
            try
            {
                string respMessage = string.Empty;
                if (supplier.Id == 0)
                {
                   
                    _unitOfWork.SupplierRepository.Add(supplier);
                    respMessage = "Successfully Saved";
                }
                else
                {
                    supplier.CreatedDate = DateTime.Now;
                    _unitOfWork.SupplierRepository.Update(supplier);
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

        public string DeleteSupplier(int Id)
        {
            var supplier = _unitOfWork.SupplierRepository.GetById(Id);
            _unitOfWork.SupplierRepository.Delete(supplier);
            _unitOfWork.Save();
            return "Successfully Deleted";
        }
    }
}
