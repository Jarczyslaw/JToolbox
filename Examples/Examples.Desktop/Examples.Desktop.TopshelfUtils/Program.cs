namespace Examples.Desktop.TopshelfUtils
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FooService fooService = new();

            fooService.Run();
        }
    }
}