using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Bases;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
