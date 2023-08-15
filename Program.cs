using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Token
{
    public string Type { get; }
    public string Lexeme { get; }

    public Token(string type, string lexeme)
    {
        Type = type;
        Lexeme = lexeme;
    }
}

class Scanner
{
    private readonly List<(string TokenType, Regex Pattern)> tokenDefinitions = new List<(string, Regex)>
    {
        ("RECIPE", new Regex(@"\bRECIPE\b")),
        ("METHOD", new Regex(@"\bMETHOD\b")),
        ("INTEGER", new Regex(@"\bINTEGER\b")),
        ("INGREDIENT", new Regex(@"\bINGREDIENT\b")),
        ("FLOAT", new Regex(@"\bFLOAT\b")),
        ("SPEAK", new Regex(@"\bSPEAK\b")),
        ("SHARE", new Regex(@"\bSHARE\b")),
        ("WHILE", new Regex(@"\bWHILE\b")),
        ("DONE", new Regex(@"\bDONE\b")),
        ("LOOP", new Regex(@"\bLOOP\b")),
        ("ELSE", new Regex(@"\bELSE\b")),
        ("ASK", new Regex(@"\bASK\b")),
        ("AS", new Regex(@"\bAS\b")),
        ("DO", new Regex(@"\bDO\b")),
        ("IF", new Regex(@"\bIF\b")),
        ("PLUS", new Regex(@"\+")),

};

    public List<Token> Tokenize(string input)
    {
        List<Token> tokens = new List<Token>();
        int currentPosition = 0;

        while (currentPosition < input.Length)
        {
            bool matched = false;
            foreach ((string tokenType, Regex pattern) in tokenDefinitions)
            {
                Match match = pattern.Match(input, currentPosition);
                if (match.Success && match.Index == currentPosition)
                {
                    matched = true;
                    tokens.Add(new Token(tokenType, match.Value));
                    currentPosition += match.Length;
                    break;
                }
            }

            if (!matched)
            {
                // Skip unrecognized characters
                currentPosition++;
            }
        }

        return tokens;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string input = "RECIPE HelloWorld DO METHOD Main() AS INTEGER DO SPEAK(\"Hello, World!\"); DONE DONE";

        Scanner scanner = new Scanner();
        List<Token> tokens = scanner.Tokenize(input);

        foreach (Token token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Lexeme: {token.Lexeme}");
        }
    }
}
