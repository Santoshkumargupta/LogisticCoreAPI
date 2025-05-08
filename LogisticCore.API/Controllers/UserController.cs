using LogisticCore.Api.Controllers;
using LogisticCore.Application.DTO;
using LogisticCore.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticCore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;
        private ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService )
        {
            _userService = userService;
            _tokenService = tokenService;
          
        }

        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();

        //    return GetApiResponse(users);
        //}

        [HttpGet("userDetail")]
        public IActionResult UserDetail()
        {
            var claimsPrincipal = HttpContext.User;
            var userDetail = _tokenService.GetCurrentUser(claimsPrincipal);
            var user = _userService.GetById(userDetail.Id);
            return GetApiResponse(user);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> UserRegistrations([FromBody] UserDTO model)
        {

            var result = await _userService.UserRegistration(model);
            return GetApiResponse(result);
        }




        [HttpPost("changePassword")]
        public IActionResult changePassword(string OldPassword, string NewPassword)
        {
            var claimsPrincipal = HttpContext.User;
            var user = _tokenService.GetCurrentUser(claimsPrincipal);
            var result = _userService.ChangePassword(user.Id, OldPassword, NewPassword);
            return GetApiResponse(result);
        }

      

     

      


      

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
