using EntityFramework.BusinessLogic.Models.Common;

namespace EntityFramework.BusinessLogic.Models
{
    public class Student : BaseExtendedModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}