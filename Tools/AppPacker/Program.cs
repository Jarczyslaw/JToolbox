using Newtonsoft.Json;
using System;

namespace AppPacker
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("AppPacker started");

            try
            {
                var parser = new CommandLineParser();
                var config = parser.Parse(args);

                if (config != null)
                {
                    Console.WriteLine($"Loaded config: {JsonConvert.SerializeObject(config)}");

                    var packer = new Packer();
                    packer.Run(config);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.WriteLine("AppPacker finished");
        }
    }
}