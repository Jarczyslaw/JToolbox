namespace Examples.Desktop.Base.ViewModels
{
    public class SelectItemViewModel
    {
        public string Display => Value.ToString();
        public object Value { get; set; }
    }
}