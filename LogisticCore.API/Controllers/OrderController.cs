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
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() 
        {
            var result = _orderService.GetAll();
            return GetApiResponse(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int Id)
        {

            var result = _orderService.GetById(Id);
            return GetApiResponse(result);
        }


        [HttpPost("addAndUpdate")]
        public IActionResult AddAndUpdate(Order model)
        {

            var result = _orderService.AddAndUpdateOrder(model);
            return GetApiResponse(result);
        }


        [HttpPost("deleteOrder")]
        public IActionResult DeleteOrder(int Id)
        {

            var result = _orderService.DeleteOrder(Id);
            return GetApiResponse(result);
        }
    }
}
