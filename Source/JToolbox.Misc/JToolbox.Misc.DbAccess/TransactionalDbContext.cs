using System.Data;

namespace JToolbox.Misc.DbAccess
{
    public class TransactionalDbContext : DbContext
    {
        public TransactionalDbContext(IDbFactory factory)
            : base(factory)
        {
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
            CommitTransaction = true;
        }

        public bool CommitTransaction { get; set; }
        public IDbTransaction Transaction { get; set; }

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