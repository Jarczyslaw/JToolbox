using System;

namespace EntityFramework.BusinessLogic.Models.Common
{
    public class BaseExtendedModel : BaseModel
    {
        public DateTime CreateDate { get; set; }

        public bool Deleted { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}