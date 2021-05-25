using JToolbox.WinForms.MVP;

namespace Examples.Desktop.MVP.Forms
{
    public delegate void Pass(bool modal, string value);

    public interface IMainView : IView
    {
        event Pass OnPass;
    }
}