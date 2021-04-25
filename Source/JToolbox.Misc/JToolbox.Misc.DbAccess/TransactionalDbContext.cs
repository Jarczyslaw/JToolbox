using System.Data;

namespace JToolbox.Misc.DbAccess
{
    public class TransactionalDbContext : DbContext
    {
        public IDbTransaction Transaction { get; set; }
        public bool CommitTransaction { get; set; }

        public TransactionalDbContext(IDbFactory factory)
            : base(factory)
        {
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
            CommitTransaction = true;
        }

        public override void Dispose()
        {
            if (CommitTransaction)
            {
                Transaction.Commit();
            }
            else
            {
                Transaction.Rollback();
            }
            Transaction.Dispose();
            base.Dispose();
        }
    }
}