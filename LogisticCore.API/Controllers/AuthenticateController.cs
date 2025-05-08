using log4net;
using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using LogisticCore.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnauthorizedAccessException = LogisticCore.Core.Exceptions.UnauthorizedAccessException;

namespace LogisticCore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController
    {
        private readonly IAccountService _accountService;

        private static ILog _log = LogManager.GetLogger(typeof(AuthenticateController));
        public AuthenticateController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _accountService.Authenticate(model, ipAddress());

            return GetApiResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromHeader] string refreshToken)
        {
            var refreshTokens = refreshToken?? Request.Headers["refreshToken"];
            var response = _accountService.RefreshToken(refreshTokens, ipAddress());
            if (response == null)
                throw new UnauthorizedAccessException("Invalid token");
            return GetApiResponse(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromHeader] string revokeToken)
        {
            // accept token from request body or cookie
            var token = revokeToken ?? Request.Headers["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new BadRequestException("Token is required");

            var response = _accountService.RevokeToken(token, ipAddress());

            if (!response)
                throw new NotFoundException("Token not found");

            return GetApiResponse(response);
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
