namespace MathExpressionParser;

enum TokenType
{
    Delimiter,
    Number
}

class Token
{
    public string Value { get; set; } = null!;
    public TokenType? Type { get; set; }

    public override string ToString()
    {
        return $"Value: {Value}, Type: {Type}";
    }
}
