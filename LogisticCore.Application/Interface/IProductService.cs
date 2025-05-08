using LogisticCore.Domain.Model;

namespace LogisticCore.Application.Interface
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int Id);
        string AddAndUpdateProduct(Product model);
        string DeleteProduct(int Id);
    }
}
