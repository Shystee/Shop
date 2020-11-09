using Shop.DataAccess;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
    }

    public class RefreshTokenRepository
            : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DataContext context)
                : base(context)
        {
        }
    }
}