using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Context;
using LogisticCore.Infrastructure.DbAccess;
using LogisticCore.Infrastructure.Interface;

namespace LogisticCore.Infrastructure.Repository
{
    public class RefreshTokenRepository: GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly LogisticCoreContext _dbContext;
        public RefreshTokenRepository(LogisticCoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RefreshToken> GetAllRefreshTokenByUserIdandIpAddress(int Id, string ipaddress)
        {
            try
            {
                var result = _dbContext.RefreshTokens.Where(x => x.UserId == Id || x.CreatedByIp == ipaddress).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RefreshToken GetRefreshTokenByToken(string token)
        {
            try
            {
                var result = _dbContext.RefreshTokens.Where(x => x.Token == token).OrderByDescending(x => x.CreatedAt).First();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RefreshToken> GetRefreshTokenbyUserId(int Id)
        {
            try
            {
                var result = _dbContext.RefreshTokens.Where(x => x.UserId == Id).OrderByDescending(x => x.CreatedAt).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
