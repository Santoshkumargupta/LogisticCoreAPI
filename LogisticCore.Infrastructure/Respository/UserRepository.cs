using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Context;
using LogisticCore.Infrastructure.DbAccess;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LogisticCoreContext _dbContext;
        public UserRepository(LogisticCoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public User GetUserDetailByUserName(string UserName)
        //{
        //    return _dbContext.Users.First(x => x.UserName == UserName);
        //}


        public User GetUserDetailByUserName(string UserName)
        {
            var result = _dbContext.Users.FirstOrDefault(x => x.UserName == UserName); 
            return result;
        }

        public User GetUserByToken(string token)
        {
            var userId = _dbContext.RefreshTokens.Where(x => x.Token == token).OrderByDescending(x => x.CreatedAt).First().UserId;
            var result = _dbContext.Users.Where(x => x.Id == userId).First();
            return result;
        }

        public User GetUserDetailByPassword(string password)
        {


            var result = _dbContext.Users.Where(x => x.Password == password).FirstOrDefault();
            return result;
        }

    }
}
