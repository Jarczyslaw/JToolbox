﻿using JToolbox.DataAccess.L2DB.Abstraction;
using LinqToDB.Mapping;
using System;

namespace JToolbox.DataAccess.L2DB.MySql.Entities
{
    public abstract class BaseExtendedEntity : BaseEntity, IBaseExtendedEntity
    {
        [Column(Precision = 3), NotNull]
        public DateTime CreateDate { get; set; }

        [Column]
        public int? CreateUser { get; set; }

        [Column, NotNull]
        public bool IsDeleted { get; set; }

        [Column(Precision = 3), NotNull]
        public DateTime UpdateDate { get; set; }

        [Column]
        public int? UpdateUser { get; set; }
    }
}