namespace JToolbox.Core.Tests.ObjectPoolSet
{
    public class DbContext
    {
        public DbContext(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool IsInUse { get; set; }
    }
}