using LogisticCore.Application.DTO;
using LogisticCore.Application.Mapper;
using LogisticCore.Core.Helper;
using LogisticCore.Domain.Model;
using Microsoft.Extensions.Options;
using LogisticCore.Application.Interface;
using LogisticCore.Core.Exceptions;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppSettings _appSettings;

        public UserService(
               IUnitOfWork unitOfWork,
               IOptions<AppSettings> appSettings

               )
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;

        }


        public IEnumerable<UserDTO> GetAll()
        {
            var userList = _unitOfWork.UserRepository.GetAll().ToList();
            var userDTOList = UserMapper.GetAllUserDTO(userList);
            return userDTOList;
        }

        public UserDTO GetById(int id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);

            return UserMapper.GetUserDTO(user);
        }

        public async Task<string> UserRegistration(UserDTO model)
        {


            try
            {


                var user = new User
                {
                    FullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RoleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    Dob = model.Dob,
                };

                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.Save();


                return "Successfully Registered";
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        }

      

       
        public List<RefreshToken> GetRefreshTokenByUserId(int UserId)
        {
            var result = _unitOfWork.RefreshTokenRepository.GetRefreshTokenbyUserId(UserId);
            if (result == null)
            {
                throw new NotFoundException("UserId Not Found");
            }
            return result;
        }

        public string ChangePassword(int userId,string OldPassword, string NewPassword)
        {


            var userDetails = _unitOfWork.UserRepository.GetById(userId);


            bool isverified = BCrypt.Net.BCrypt.Verify(OldPassword, userDetails.Password);
            if (!isverified)
            {
                throw new BadRequestException("Incorrect current password");
            }

            userDetails.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            _unitOfWork.UserRepository.Update(userDetails);
            _unitOfWork.Save();
            return "Password successfully changed";

        }

     

    }  // helper methods
}




