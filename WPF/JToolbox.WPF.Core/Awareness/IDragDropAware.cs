namespace JToolbox.WPF.Core.Awareness
{
    public interface IDragDropAware
    {
        void OnDrag(object source);
        void OnDrop(object source, object target);
    }
}