using EntityFramework.DataAccess.Models;
using JToolbox.DataAccess.EF;
using System.Data.Entity;

namespace EntityFramework.DataAccess
{
    public class Context : BaseContext
    {
        public Context() : base("ExampleAppConnectionString")
        {
        }

        public DbSet<AssessmentEntity> Assessments { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<StudentGroupEntity> StudentsGroups { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
            modelBuilder.HasDefaultSchema(nameof(Context));

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(x => x.Students)
                .WithMany(x => x.Subjects)
                .Map(x => x.ToTable("StudentsSubjects").MapLeftKey("SubjectId").MapRightKey("StudentId"));

            modelBuilder.Entity<StudentGroupEntity>()
                .HasOptional(x => x.Leader)
                .WithMany()
                .HasForeignKey(x => x.LeaderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}