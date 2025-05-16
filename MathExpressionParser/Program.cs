using MathExpressionParser;

ExpressionParser parser = new();

while (true)
{
    Console.WriteLine("Please insert an expression (or 'quit' to exit): ");

    double result;
    string expression = Console.ReadLine() ?? string.Empty;

    if (expression.ToLower() == "quit")
        break;

    try
    {
        result = parser.Parse(expression);
        Console.WriteLine($"Result: {result}");
        Console.WriteLine(string.Empty);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}