using System.Collections.Generic;

namespace JToolbox.Core.Helpers.CollectionsMerge
{
    public class CollectionsMergeResult<TNew, TOld>
    {
        public List<TNew> ToAdd { get; set; } = new List<TNew>();

        public List<TOld> ToDelete { get; set; } = new List<TOld>();

        public List<CollectionsMergeResultUpdateEntry<TNew, TOld>> ToUpdate { get; set; } = new List<CollectionsMergeResultUpdateEntry<TNew, TOld>>();
    }
}