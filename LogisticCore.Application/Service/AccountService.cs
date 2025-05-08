using LogisticCore.Application.DTO;
using LogisticCore.Application.Mapper;
using LogisticCore.Core.Helper;
using LogisticCore.Core.Model;
using LogisticCore.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using UnauthorizedAccessException = LogisticCore.Core.Exceptions.UnauthorizedAccessException;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppSettings _appSettings;
        private readonly ITokenService _tokenService;
        public AccountService(IUnitOfWork unitOfWork,
               IOptions<AppSettings> appSettings,
               ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;

            _appSettings = appSettings.Value;
            _tokenService = tokenService;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _unitOfWork.UserRepository.GetUserDetailByUserName(model.UserName);

            if (user == null || string.IsNullOrEmpty(user.Password))
            {
                throw new BadRequestException("This account is not activated. Please activate the account.");
            }

            bool isVerify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isVerify)
            {
                throw new BadRequestException("Username or password is incorrect");
            }

            var userDTO = UserMapper.GetUserDTO(user);


            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _tokenService.generateJwtToken(userDTO);
            var refreshToken = _tokenService.generateRefreshToken(ipAddress);

            refreshToken.UserId = userDTO.Id;
            // save refresh token
            _unitOfWork.RefreshTokenRepository.Add(refreshToken);
            _unitOfWork.Save();
            var authResponse = _tokenService.GetAuthenticateResponse(userDTO, jwtToken, refreshToken.Token);

            return authResponse;

        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = _unitOfWork.UserRepository.GetUserByToken(token);

            // return null if no user found with token
            if (user == null) return null;
            var userDTO = UserMapper.GetUserDTO(user);
            var refreshToken = _unitOfWork.RefreshTokenRepository.GetRefreshTokenByToken(token);

            // return null if token is no longer active
            if (!refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid token");
            // replace old refresh token with a new one and save

            var newRefreshToken = new RefreshToken();
            if (refreshToken.ExpiresAt >= DateTime.UtcNow)
            {
                newRefreshToken = _tokenService.generateRefreshToken(ipAddress);
                newRefreshToken.UserId = userDTO.Id;
                refreshToken.RevokedAt = DateTime.UtcNow;
                refreshToken.RevokedByIp = ipAddress;
                refreshToken.ReplacedByToken = newRefreshToken.Token;
                refreshToken.IsActive = false;
                _unitOfWork.RefreshTokenRepository.Update(refreshToken);
                _unitOfWork.RefreshTokenRepository.Add(newRefreshToken);
                _unitOfWork.Save();
                var jwtToken = _tokenService.generateJwtToken(userDTO);
                return _tokenService.GetAuthenticateResponse(userDTO, jwtToken, newRefreshToken.Token);

            }
            else
            {
                throw new UnauthorizedAccessException("Refresh token has expired");
            }

        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _unitOfWork.UserRepository.GetUserByToken(token); /* _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));*/

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = _unitOfWork.RefreshTokenRepository.GetRefreshTokenByToken(token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.IsExpired = true;
            refreshToken.IsActive = true;
            _unitOfWork.RefreshTokenRepository.Update(refreshToken);

            return true;
        }

    }
}
