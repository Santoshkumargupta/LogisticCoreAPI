using LogisticCore.Application.DTO;
using LogisticCore.Domain.Model;

namespace LogisticCore.Application.Mapper
{
    public static class UserMapper
    {
        public static List<UserDTO> GetAllUserDTO(List<User> userList)
        {
            return userList.Select(GetUserDTO).ToList();
        }

        public static UserDTO GetUserDTO(User model)
        {
            return new UserDTO()
            {
                Id = model.Id,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                RoleId = model.RoleId,
                Dob = model.Dob


            };
        }

        public static User GetUserDAO(UserDTO model)
        {
            return new User()
            {
                Id = model.Id,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                RoleId = model.RoleId,
                Dob = model.Dob
                //BiometricStatus = model.BiometricStatus,


            };
        }
    }
}
