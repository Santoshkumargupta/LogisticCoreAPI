using LogisticCore.Application.DTO;
using LogisticCore.Core.Model;
using LogisticCore.Domain.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.Interface
{
    public interface ITokenService
    {
        string generateJwtToken(UserDTO user);
        RefreshToken generateRefreshToken(string ipAddress);
        AuthenticateResponse GetAuthenticateResponse(UserDTO user, string jwtToken, string RefreshToken);
        UserDTO GetCurrentUser(ClaimsPrincipal claimsPrincipal);
        string generateBioToken(string deviceId);

    }
}
