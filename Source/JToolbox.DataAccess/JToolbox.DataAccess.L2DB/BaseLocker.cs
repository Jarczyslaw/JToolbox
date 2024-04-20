namespace JToolbox.DataAccess.L2DB
{
    public class BaseLocker
    {
        public virtual bool AcquireLock(string connectionString) => true;

        public virtual void ReleaseLock()
        { }
    }
}