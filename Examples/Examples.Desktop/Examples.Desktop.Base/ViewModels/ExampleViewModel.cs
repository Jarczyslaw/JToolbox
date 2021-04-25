namespace Examples.Desktop.Base.ViewModels
{
    public class ExampleViewModel
    {
        public IDesktopExample Example { get; set; }
        public string Display => Example.Title;
    }
}