namespace EntityFramework.DataAccess
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("Context.StudentsSubjects", "StudentId", "Context.Students");
            DropForeignKey("Context.StudentsSubjects", "SubjectId", "Context.Subjects");
            DropForeignKey("Context.Assessments", "SubjectId", "Context.Subjects");
            DropForeignKey("Context.Students", "StudentsGroupId", "Context.StudentGroups");
            DropForeignKey("Context.StudentGroups", "LeaderId", "Context.Students");
            DropForeignKey("Context.Assessments", "StudentId", "Context.Students");
            DropIndex("Context.StudentsSubjects", new[] { "StudentId" });
            DropIndex("Context.StudentsSubjects", new[] { "SubjectId" });
            DropIndex("Context.StudentGroups", new[] { "LeaderId" });
            DropIndex("Context.Students", new[] { "StudentsGroupId" });
            DropIndex("Context.Assessments", new[] { "SubjectId" });
            DropIndex("Context.Assessments", new[] { "StudentId" });
            DropTable("Context.StudentsSubjects");
            DropTable("Context.Subjects");
            DropTable("Context.StudentGroups");
            DropTable("Context.Students");
            DropTable("Context.Assessments");
        }

        public override void Up()
        {
            CreateTable(
                "Context.Assessments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Notes = c.String(),
                    StudentId = c.Int(nullable: false),
                    SubjectId = c.Int(nullable: false),
                    Value = c.Int(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Context.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("Context.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);

            CreateTable(
                "Context.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 100),
                    LastName = c.String(nullable: false, maxLength: 100),
                    StudentsGroupId = c.Int(),
                    CreateDate = c.DateTime(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Context.StudentGroups", t => t.StudentsGroupId)
                .Index(t => t.StudentsGroupId);

            CreateTable(
                "Context.StudentGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LeaderId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 100),
                    CreateDate = c.DateTime(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Context.Students", t => t.LeaderId)
                .Index(t => t.LeaderId);

            CreateTable(
                "Context.Subjects",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    CreateDate = c.DateTime(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Context.StudentsSubjects",
                c => new
                {
                    SubjectId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SubjectId, t.StudentId })
                .ForeignKey("Context.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("Context.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentId);
        }
    }
}