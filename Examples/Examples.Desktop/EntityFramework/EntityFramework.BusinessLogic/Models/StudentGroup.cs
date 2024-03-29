﻿using EntityFramework.BusinessLogic.Models.Common;
using System.Collections.Generic;

namespace EntityFramework.BusinessLogic.Models
{
    public class StudentGroup : BaseExtendedModel
    {
        public Student Leader { get; set; }

        public int? LeaderId { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}