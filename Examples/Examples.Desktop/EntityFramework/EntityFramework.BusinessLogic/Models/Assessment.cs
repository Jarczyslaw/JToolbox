using EntityFramework.BusinessLogic.Models.Common;

namespace EntityFramework.BusinessLogic.Models
{
    public class Assessment : BaseExtendedModel
    {
        public string Notes { get; set; }

        public Student Student { get; set; }

        public int StudentId { get; set; }

        public Subject Subject { get; set; }

        public int SubjectId { get; set; }

        public int Value { get; set; }
    }
}