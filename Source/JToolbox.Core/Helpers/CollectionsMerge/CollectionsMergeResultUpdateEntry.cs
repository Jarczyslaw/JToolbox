namespace JToolbox.Core.Helpers.CollectionsMerge
{
    public class CollectionsMergeResultUpdateEntry<TNew, TOld>
    {
        public TNew NewItem { get; set; }

        public TOld OldItem { get; set; }
    }
}