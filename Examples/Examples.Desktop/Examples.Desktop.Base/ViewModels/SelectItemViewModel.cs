namespace Examples.Desktop.Base.ViewModels
{
    public class SelectItemViewModel
    {
        public object Value { get; set; }
        public string Display => Value.ToString();
    }
}