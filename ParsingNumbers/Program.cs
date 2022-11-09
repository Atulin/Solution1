var arr = new[] {"0", null, "179", "kookaburra"};
var functions = new (string name, ConsoleColor color, Action<string?> fun)[]
{
    (nameof(WithConvert), ConsoleColor.Green, WithConvert),
    (nameof(WithParse), ConsoleColor.Red, WithParse),
    (nameof(WithTryParse), ConsoleColor.Blue, WithTryParse)
};

foreach (var fun in functions)
{
    Console.ForegroundColor = fun.color;
    Console.WriteLine(fun.name);
    foreach (var num in arr)
    {
        Console.Write($"| {num ?? "null",-10} | ");
        fun.fun(num);
    }
    Console.ResetColor();
}


void WithConvert(string? input)
{
    try
    {
        var num = Convert.ToInt32(input);
        Console.WriteLine($"Your number times two is {num * 2}");
    }
    catch
    {
        Console.WriteLine("An exception has occurred");
    }
}


void WithParse(string? input)
{
    try
    {
        var num = int.Parse(input);
        Console.WriteLine($"Your number times two is {num * 2}");
    }
    catch
    {
        Console.WriteLine("An exception has occurred");
    }
}


void WithTryParse(string? input)
{
    if (int.TryParse(input, out var num))
    {
        Console.WriteLine($"Your number times two is {num * 2}");
    }
    else
    {
        Console.WriteLine("Not a number");
    }
    
}