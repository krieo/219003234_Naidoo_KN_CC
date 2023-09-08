using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _219003234_Naidoo_KN_CC
{
    public class Scanner
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
            ("STRINGLIT", new Regex(@"""(?:[^""\\]*(?:\\.[^""\\]*)*)""")),
            ("INTEGERLIT", new Regex(@"[0-9]+")),
            ("FLOATLIT", new Regex(@"[0-9]*\.[0-9]+")),
            ("BOOLLITTRUE", new Regex(@"\bTRUE\b")),
            ("BOOLLITFALSE", new Regex(@"\bFALSE\b")),
            ("ARRAY", new Regex(@"\bARRAY\b")),
            ("ID", new Regex(@"[a-zA-Z][a-zA-Z0-9]*")),
            ("WHITESPACE", new Regex(@"\s+")),
            ("COMMENT", new Regex(@"//.*?(?=\r|\n|$)", RegexOptions.Singleline)),
            ("ERROR", new Regex(@"\S")),
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
                    tokens.Add(longestMatch);
                    currentPosition += maxLength;
                }
                else
                {
                    // Unrecognized character
                    Console.WriteLine($"Unrecognized character at position {currentPosition}: '{input[currentPosition]}'");
                    currentPosition++; // Move to the next character
                }
            }

            return tokens;
        }
    }
}
