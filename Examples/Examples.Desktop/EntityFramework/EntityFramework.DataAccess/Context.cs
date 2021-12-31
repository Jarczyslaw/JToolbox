using EntityFramework.DataAccess.Models;
using JToolbox.DataAccess.EF;
using System.Data.Entity;

namespace EntityFramework.DataAccess
{
    public class Context : BaseContext
    {
        public Context() : base("EFApp")
        {
        }

        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentsGroup> StudentsGroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
            modelBuilder.HasDefaultSchema(nameof(Context));

            modelBuilder.Entity<Subject>()
                .HasMany(x => x.Students)
                .WithMany(x => x.Subjects)
                .Map(x =>
                {
                    x.MapLeftKey("StudentId");
                    x.MapRightKey("SubjectId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}