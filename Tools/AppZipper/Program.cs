using Newtonsoft.Json;
using System;

namespace AppZipper
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("AppZipper started");

            try
            {
                var parser = new CommandLineParser();
                var config = parser.Parse(args);

                if (config != null)
                {
                    Console.WriteLine($"Loaded config: {JsonConvert.SerializeObject(config)}");

                    if (!config.Validate())
                    {
                        return;
                    }

                    var packer = new Zipper();
                    packer.Run(config);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.WriteLine("AppZipper finished");
        }
    }
}