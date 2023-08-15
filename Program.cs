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
      ("MINUS", new Regex(@"\-")),
    ("FORWARD SLASH", new Regex(@"/")),
    ("STAR", new Regex(@"\*")),
    ("ASSIGN", new Regex(@"=")),
    ("EQUAL", new Regex(@"==")),
    ("NEQ", new Regex(@"<>")),
    ("GREATEREQUAL", new Regex(@">=")),
    ("LESSEREQUAL", new Regex(@"<=")),
    ("GREATER", new Regex(@">")),
    ("LESSER", new Regex(@"<")),
    ("SEMICOLON", new Regex(@";")),
    ("LEFTPARENTHESIS", new Regex(@"\(")),
    ("RIGHTPARENTHESIS", new Regex(@"\)")),
    ("STRING", new Regex(@"\bSTRING\b")),
    ("BOOL", new Regex(@"\bBOOL\b")),
  ("STRINGLIT", new Regex(@"""(?:[a-zA-Z0-9~!@#$%^&*`()\[\]_\-+=|{};:<>,.?\\])*")),

    ("INTEGERLIT", new Regex(@"[0-9]+")),
    ("FLOATLIT", new Regex(@"[0-9]*\.[0-9]*")),
    ("ID", new Regex(@"([a-zA-Z]|[0-9])([a-zA-Z]|[0-9])*")),
    ("BOOLLITTRUE", new Regex(@"\bTRUE\b")),
    ("BOOLLITFALSE", new Regex(@"\bFALSE\b")),
    ("ARRAY", new Regex(@"\bARRAY\b")),

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
    static string ReadTextFromFile(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);
            return content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            return null;
        }
    }

    static void Main(string[] args)
    {
        // string input = "RECIPE HelloWorld DO METHOD Main() AS INTEGER DO SPEAK(\"Hello, World!\"); DONE DONE";
        /*
          string input = @"RECIPE MathOperations
                      DO
                      METHOD Main() AS INTEGER
                      DO
                      INGREDIENT a AS INTEGER;
                      INGREDIENT b AS INTEGER;
                      a = 14;
                      b = 3;
                      INGREDIENT sum AS INTEGER;
                      sum = a + b;
                      INGREDIENT product AS INTEGER;
                      product = a * b;
                      INGREDIENT quotient AS FLOAT;
                      quotient = a / b;
                      SPEAK(""Sum: "" + sum);
                      SPEAK(""Product: "" + product);
                      SPEAK(""Quotient: "" + quotient);
                      SHARE 0;
                      DONE
                      DONE";
         */
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string dataFolderPath = Path.Combine(projectDirectory, "sample programs");
        string filename = "sample8.txt"; 

        string filePath = Path.Combine(dataFolderPath, filename);
        Console.WriteLine(filePath);
      //  Console.ReadLine();
        string input = ReadTextFromFile(filePath);
         
        input = ReadTextFromFile(filePath);

        Scanner scanner = new Scanner();
        List<Token> tokens = scanner.Tokenize(input);

        foreach (Token token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Lexeme: {token.Lexeme}");
        }
    }
}
