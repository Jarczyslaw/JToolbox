using EntityFramework.BusinessLogic.Models;
using System.Collections.Generic;

namespace EntityFramework.BusinessLogic
{
    public interface IBusinessService
    {
        List<Assessment> GetAssessments();
        List<StudentGroup> GetStudentGroups();
        List<Student> GetStudents();
        List<Subject> GetSubjects();
    }
}