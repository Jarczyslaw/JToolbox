namespace JToolbox.DataAccess.Common
{
    public class EmptyUserIdProvider : IUserIdProvider
    {
        public int? UserId => null;
    }
}