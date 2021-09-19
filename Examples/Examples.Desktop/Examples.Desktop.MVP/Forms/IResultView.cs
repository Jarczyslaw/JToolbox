using JToolbox.WinForms.MVP;

namespace Examples.Desktop.MVP.Forms
{
    public delegate void Accept();

    public delegate void Cancel();

    public interface IResultView : IView
    {
        event Accept OnAccept;

        event Cancel OnCancel;

        string Value { set; }
    }
}