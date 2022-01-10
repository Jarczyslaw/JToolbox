using EntityFramework.BusinessLogic.Models;
using System.Collections.Generic;

namespace EntityFramework.BusinessLogic
{
    public class BusinessService : IBusinessService
    {
        public List<Assessment> GetAssessments()
        {
            return new List<Assessment>
            {
                new Assessment
                {
                    Id = 1,
                    Value = 5,
                },
                new Assessment
                {
                    Id = 2,
                    Value = 4,
                },
                new Assessment
                {
                    Id = 3,
                    Value = 3,
                },
            };
        }

        public List<StudentGroup> GetStudentGroups()
        {
            return new List<StudentGroup>()
            {
                new StudentGroup
                {
                    Id = 1,
                    Name = "Group1",
                },
                new StudentGroup
                {
                    Id = 2,
                    Name = "Group2",
                },
            };
        }

        public List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    FirstName = "John"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Mark"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Denis"
                },
            };
        }

        public List<Subject> GetSubjects()
        {
            return new List<Subject>
            {
                new Subject
                {
                    Id = 1,
                    Name = "Subject1"
                },
                new Subject
                {
                    Id = 2,
                    Name = "Subject2"
                }
            };
        }
    }
}