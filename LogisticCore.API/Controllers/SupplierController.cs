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
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _supplierService.GetAll();
            return GetApiResponse(result);
        }


        [HttpGet("getById")]
        public IActionResult GetById(int Id) {
        
         var result = _supplierService.GetById(Id);
            return GetApiResponse(result);
        }


        [HttpPost("addAndUpdate")]
        public IActionResult AddAndUpdate(Supplier model )
        {

            var result = _supplierService.AddAndUpdateSupplier(model);
            return GetApiResponse(result);
        }


        [HttpPost("deleteSupplier")]
        public IActionResult DeleteSupplier(int Id)
        {

            var result = _supplierService.DeleteSupplier(Id);
            return GetApiResponse(result);
        }
    }
}
