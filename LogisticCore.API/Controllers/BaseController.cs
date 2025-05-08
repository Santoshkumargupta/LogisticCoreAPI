using LogisticCore.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticCore.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        [NonAction]
        public IActionResult GetApiResponse(Object model)
        {

            if (model == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Data = new List<Object>(),
                    Message = "Not Content Found"

                });
            }
            return Ok(new ApiResponse<Object> { StatusCode = (int)HttpStatusCode.OK, Data = model, Message = "Success" });
        }

    }
}
