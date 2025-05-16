namespace MathExpressionParser;

class ExpressionParser
{
    Tokenizer _tokenizer = null!;
    Token? _currentToken = null!;

    private void MoveToNextToken()
    {
        _currentToken = _tokenizer.GetNextToken();
    }

    private double ParseTerm()
    {
        double result = ParseFactor();
        if (_currentToken == null)
            return result;

        while (_currentToken?.Value == "+"
                || _currentToken?.Value == "-")
        {
            string op = _currentToken.Value;

            MoveToNextToken();
            if (_currentToken == null)
                throw new ApplicationException("Syntax error");

            double rightOperand = ParseFactor();
            switch (op)
            {
                case "-":
                    result = result - rightOperand;
                    break;

                case "+":
                    result = result + rightOperand;
                    break;
            }
        }

        return result;
    }

    private double ParseFactor()
    {
        double result = ParseExponent();
        if (_currentToken == null)
            return result;

        while (_currentToken?.Value == "*"
                    || _currentToken?.Value == "/"
                    || _currentToken?.Value == "%")
        {
            string op = _currentToken.Value;

            MoveToNextToken();
            if (_currentToken == null)
                throw new ApplicationException("Syntax error");

            double rightOperand = ParseExponent();
            switch (op)
            {
                case "*":
                    result = result * rightOperand;
                    break;

                case "/":
                    if (rightOperand == 0)
                        throw new ApplicationException("Division by zero");
                    result = result / rightOperand;
                    break;

                case "%":
                    result = result % rightOperand;
                    break;
            }
        }

        return result;
    }


    private double ParseExponent()
    {
        double result = ParseUnary();
        if (_currentToken == null)
            return result;

        if (_currentToken.Value == "^")
        {
            string op = _currentToken.Value;

            MoveToNextToken();
            if (_currentToken == null)
                throw new ApplicationException("Syntax error");

            double rightOperand = ParseExponent();

            if (rightOperand == 0)
            {
                return 1;
            }
            return Math.Pow(result, rightOperand);
        }

        return result;
    }

    private double ParseUnary()
    {
        if (_currentToken == null)
            throw new ApplicationException("Syntax error");

        string op = string.Empty;
        if (_currentToken.Value == "+"
            || _currentToken.Value == "-")
        {
            op = _currentToken.Value;
        }

        double result = ParseParenthesis();
        return (op == "-") ? -result : result;
    }

    private double ParseParenthesis()
    {
        if (_currentToken == null)
            throw new ApplicationException("Syntax error");

        if (_currentToken.Value == "(")
        {
            MoveToNextToken();
            if (_currentToken == null)
                throw new ApplicationException("Syntax error");

            double result = ParseTerm();

            if (_currentToken.Value != ")")
                throw new ApplicationException("Syntax error");

            MoveToNextToken();

            return result;
        }
        else
        {
            return ParseNumber();
        }
    }

    private double ParseNumber()
    {
        if (_currentToken == null)
            throw new ApplicationException("Syntax error");

        if (_currentToken.Type == TokenType.Number)
        {
            double val = double.Parse(_currentToken.Value);
            MoveToNextToken();
            return val;
        }

        throw new ApplicationException("Syntax error");
    }


    public double Parse(string expression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new ApplicationException("Cannot evaluate an empty expression");

        _tokenizer = new(expression);

        MoveToNextToken();
        if (_currentToken == null)
            throw new ApplicationException("Syntax error");

        return ParseTerm();
    }

}
