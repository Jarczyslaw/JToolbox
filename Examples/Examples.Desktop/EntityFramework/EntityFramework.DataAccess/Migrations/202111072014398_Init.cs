namespace EntityFramework.DataAccess
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("Context.SubjectStudents", "SubjectId", "Context.Students");
            DropForeignKey("Context.SubjectStudents", "StudentId", "Context.Subjects");
            DropForeignKey("Context.Assessments", "SubjectId", "Context.Subjects");
            DropForeignKey("Context.Students", "StudentsGroupId", "Context.StudentsGroups");
            DropForeignKey("Context.Assessments", "StudentId", "Context.Students");
            DropIndex("Context.SubjectStudents", new[] { "SubjectId" });
            DropIndex("Context.SubjectStudents", new[] { "StudentId" });
            DropIndex("Context.Students", new[] { "StudentsGroupId" });
            DropIndex("Context.Assessments", new[] { "SubjectId" });
            DropIndex("Context.Assessments", new[] { "StudentId" });
            DropTable("Context.SubjectStudents");
            DropTable("Context.Subjects");
            DropTable("Context.StudentsGroups");
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
                .ForeignKey("Context.StudentsGroups", t => t.StudentsGroupId)
                .Index(t => t.StudentsGroupId);

            CreateTable(
                "Context.StudentsGroups",
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
                "Context.SubjectStudents",
                c => new
                {
                    StudentId = c.Int(nullable: false),
                    SubjectId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.StudentId, t.SubjectId })
                .ForeignKey("Context.Subjects", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("Context.Students", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
        }
    }
}