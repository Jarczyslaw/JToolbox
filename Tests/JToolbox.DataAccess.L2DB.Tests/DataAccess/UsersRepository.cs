using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.L2DB.Repositories;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    internal class UsersRepository : BaseExtendedRepository<User>
    {
        public UsersRepository(
            ITimeProvider timeProvider,
            IUserIdProvider loggedUserIdProvider)
            : base(timeProvider, loggedUserIdProvider)
        {
        }
    }
}