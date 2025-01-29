using System;

namespace JToolbox.Core.Cache
{
    public class UpdateIndicators
    {
        public int? LastId { get; set; }

        public DateTime? LastModificationDate { get; set; }

        public int? NewLastId { get; set; }

        public DateTime? NewLastModificationDate { get; set; }

        public UpdateMode UpdateMode { get; set; }
    }
}