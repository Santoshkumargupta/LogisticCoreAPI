using LogisticCore.Domain.Model;

namespace LogisticCore.Infrastructure.Interface
{
    public interface IRefreshTokenRepository: IRepository<RefreshToken>
    {
        List<RefreshToken> GetAllRefreshTokenByUserIdandIpAddress(int Id, string ipaddress);
        RefreshToken GetRefreshTokenByToken(string token);
        List<RefreshToken> GetRefreshTokenbyUserId(int Id);

    }
}
