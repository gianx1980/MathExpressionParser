using System.Text;

namespace MathExpressionParser;

class Tokenizer
{
    private string _expression;
    private int _position;

    private bool IsDelimiter(char c)
    {
        return c == '+' || c == '-' || c == '*'
                || c == '/' || c == '(' || c == ')'
                || c == '%' || c == '^';
    }

    public Tokenizer(string expression)
    {
        _expression = expression;
    }

    public Token? GetNextToken()
    {
        bool EndOfString() => _position > (_expression.Length - 1);

        // Check for end of expression
        if (EndOfString())
            return null;

        // Skip whitespaces
        while (!EndOfString() && char.IsWhiteSpace(_expression[_position]))
            _position++;

        if (IsDelimiter(_expression[_position]))
        {
            // Char is a delimiter
            Token token = new Token
            {
                Value = _expression[_position].ToString(),
                Type = TokenType.Delimiter
            };
            _position++;
            return token;
        }
        else if (char.IsDigit(_expression[_position]))
        {
            // Digit
            StringBuilder temp = new StringBuilder();
            temp.Append(_expression[_position]);
            _position++;

            while (!EndOfString()
                        && !IsDelimiter(_expression[_position])
                        && !char.IsWhiteSpace(_expression[_position])
                  )
            {
                temp.Append(_expression[_position]);
                _position++;
            }

            Token token = new Token
            {
                Value = temp.ToString().Trim(),
                Type = TokenType.Number
            };

            return token;
        }
        else
        {
            throw new ApplicationException($"Unexpected character {_expression[_position]}");
        }
    }
}