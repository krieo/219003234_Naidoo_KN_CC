using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _219003234_Naidoo_KN_CC
{
    /// <summary>
    /// This class is the scanner for the language
    /// </summary>
    public class Scanner
    {
        private readonly List<(string TokenType, Regex Pattern)> tokenDefinitions = new List<(string, Regex)>
{
    // Keywords
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
    ("END", new Regex(@"\bEND\b")), // Added for closing constructs
    ("TRUE", new Regex(@"\bTRUE\b")), // Added for boolean literals
    ("FALSE", new Regex(@"\bFALSE\b")), // Added for boolean literals
    ("ARRAY", new Regex(@"\bARRAY\b")),
    ("STRING", new Regex(@"\bSTRING\b")),
    ("BOOL", new Regex(@"\bBOOL\b")),
    ("ARRAY_DECL", new Regex(@"\bARRAY_DECL\b")), // Added for array declarations
    ("ARRAY_ASSIGNMENT", new Regex(@"\bARRAY_ASSIGNMENT\b")), // Added for array assignments

    // Operators
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
    ("LEFTBRACKET", new Regex(@"\[")), // Added for array access
    ("RIGHTBRACKET", new Regex(@"\]")), // Added for array access
    ("STRINGLIT", new Regex(@"""(?:[a-zA-Z0-9~!@#$%^&*`()\[\]_\-+=|{};:<>,.?\\])*")),
    ("INTEGERLIT", new Regex(@"[0-9]+")),
    ("FLOATLIT", new Regex(@"[0-9]*\.[0-9]*")),

    // Function Call (using @)
    ("FUNCTIONCALL", new Regex(@"@\w+\(")), // Matches @FunctionName(

    // Logical Operators
    ("LOGICALAND", new Regex(@"AND")),
    ("LOGICALOR", new Regex(@"OR")),

    // String Concatenation
    ("STRINGCONCAT", new Regex(@"\+")),

    // Whitespace and Comments
    ("WHITESPACE", new Regex(@"\s+")), // Matches whitespace
    ("COMMENT", new Regex(@"\/\/[^\n]*")) // Matches single-line comments
};


        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            int currentPosition = 0;

            while (currentPosition < input.Length)
            {
                int maxLength = 0;
                Token? longestMatch = null;

                foreach ((string tokenType, Regex pattern) in tokenDefinitions)
                {
                    Match match = pattern.Match(input, currentPosition);

                    if (match.Success && match.Index == currentPosition && match.Length > maxLength)
                    {
                        maxLength = match.Length;
                        longestMatch = new Token(tokenType, match.Value);
                    }
                }

                if (longestMatch != null)
                {
                    if (longestMatch.Type != "WHITESPACE" && longestMatch.Type != "COMMENT")
                    {
                        tokens.Add(longestMatch);
                    }
                    currentPosition += maxLength;
                }
                else
                {
                    // Skip unrecognized characters
                    currentPosition++;
                }
            }

            return tokens;
        }

    }
}

/*
 * 
 so just something cool about how the types matches i used word boundaries in the regular expression represented by "\b"
how it essentially works is that it says a word must match an entire word and not be apart of another word so for example
if an input is "INTEGERHELLO" normally this would match the type to an INTEGER and not an ID but because it is part
of another word it doesn't match it to INTEGER and instead matches it to ID
 
 */