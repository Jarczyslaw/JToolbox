using Examples.Desktop.Configuration;

try
{
    var config = new Config();

    Console.WriteLine($"{nameof(config.GetBoolValue)}: {config.GetBoolValue}");
    Console.WriteLine($"{nameof(config.GetIntValue)}: {config.GetIntValue}");
    Console.WriteLine($"{nameof(config.GetListValue)}: {config.GetListValue.Count}");
    Console.WriteLine($"{nameof(config.GetDictValue)}: {config.GetDictValue.Count}");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

Console.ReadKey();