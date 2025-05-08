using LogisticCore.Domain.Model;

namespace LogisticCore.Infrastructure.Interface
{
    public interface IUserRepository:IRepository<User>
    {
        public User GetUserDetailByUserName(string UserName);
        User GetUserByToken(string token);
        User GetUserDetailByPassword(string password);
      
    }
}
