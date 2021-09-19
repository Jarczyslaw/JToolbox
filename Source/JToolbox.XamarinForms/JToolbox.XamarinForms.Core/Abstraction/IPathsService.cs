namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IPaths
    {
        string InternalFolder { get; }
        string PrivateExternalFolder { get; }
        string PublicExternalFolder { get; }
    }
}