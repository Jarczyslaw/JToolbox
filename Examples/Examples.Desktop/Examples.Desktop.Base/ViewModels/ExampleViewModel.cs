namespace Examples.Desktop.Base.ViewModels
{
    public class ExampleViewModel
    {
        public string Display => $"{Example.Group} - {Example.Title}";

        public IDesktopExample Example { get; set; }
    }
}