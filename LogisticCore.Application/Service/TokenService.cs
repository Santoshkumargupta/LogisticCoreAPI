using LogisticCore.Application.DTO;
using LogisticCore.Core.Helper;
using LogisticCore.Core.Model;
using LogisticCore.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using LogisticCore.Application.Interface;

namespace LogisticCore.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }

        public string generateJwtToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId",user.RoleId.ToString()),
                    new Claim("Email",user.Email),
                    new Claim("UserName",user.UserName)

                }),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_appSettings.RefreshTokenExpiration),
                    CreatedAt = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    IsActive = true
                };
            }
        }

        public AuthenticateResponse GetAuthenticateResponse(UserDTO user, string jwtToken, string RefreshToken)
        {
            return new AuthenticateResponse
            {
                Id = user.Id,
                EmailId = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                AccessToken = jwtToken,
                RefreshToken = RefreshToken
            };
        }

        public UserDTO GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            string roleId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "RoleId").Value;
            string userName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserName").Value;
            string emailId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            return new UserDTO
            {
                Id = Convert.ToInt32(userId),
                RoleId = Convert.ToInt32(roleId),
                UserName = userName,
                Email = emailId
            };
        }

        public string generateBioToken(string deviceId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("deviceId", deviceId)
                }),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = DateTime.UtcNow.AddHours(_appSettings.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}
