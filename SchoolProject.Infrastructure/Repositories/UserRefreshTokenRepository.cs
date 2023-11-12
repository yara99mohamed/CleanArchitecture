using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _userRefreshToken;
        #endregion

        #region  Constractors
        public UserRefreshTokenRepository(ApplicationDBContext context) : base(context)
        {
            _userRefreshToken = context.Set<UserRefreshToken>();
        }

        #endregion

        #region Handle Functions

        #endregion
    }
}
