using LogisticCore.Api.Controllers;
using LogisticCore.Application.Interface;
using LogisticCore.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticCore.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() 
        {
            var result = _productService.GetAll();
            return GetApiResponse(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int Id)
        {

            var result = _productService.GetById(Id);
            return GetApiResponse(result);
        }


        [HttpPost("addAndUpdate")]
        public IActionResult AddAndUpdate(Product model)
        {

            var result = _productService.AddAndUpdateProduct(model);
            return GetApiResponse(result);
        }


        [HttpPost("deleteProduct")]
        public IActionResult DeleteProduct(int Id)
        {

            var result = _productService.DeleteProduct(Id);
            return GetApiResponse(result);
        }
    }
}
