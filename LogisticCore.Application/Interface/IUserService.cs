using LogisticCore.Application.DTO;
using LogisticCore.Core.Model;
using LogisticCore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.Interface
{
    public interface IUserService
    {

        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(int id);
        Task<string> UserRegistration(UserDTO model);
        List<RefreshToken> GetRefreshTokenByUserId(int UserId);
        string ChangePassword(int userId, string OldPassword, string NewPassword);
      
      
        //string ForgetPassword(string mobileNumber);
    }
}
