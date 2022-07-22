namespace JToolbox.XamarinForms.Dialogs
{
    public interface ILabelsProvider
    {
        string Cancel { get; }
        string Error { get; }
        string Information { get; }
        string No { get; }
        string Ok { get; }
        string PleaseWait { get; }
        string Question { get; }
        string Warning { get; }
        string Yes { get; }
    }
}