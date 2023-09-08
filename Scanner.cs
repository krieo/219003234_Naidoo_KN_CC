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
        // This list represents all of the tokens used in the language (type and lexemes)
        // The items in the list will get matched in order, which is why keywords are ordered from highest priority to lowest.
        //although i also added \b which acts as word boundaries so that a word will not get matched if it is a substring of 
        //another word so if i input "INTEGERHELLO" this will not match to integer but instead ID
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
            ("BOOLLITTRUE", new Regex(@"\bTRUE\b")),
            ("BOOLLITFALSE", new Regex(@"\bFALSE\b")),
            ("ARRAY", new Regex(@"\bARRAY\b")),
            ("ID", new Regex(@"\b([a-zA-Z]|[0-9])([a-zA-Z]|[0-9])*\b")),
        };

        /// <summary>
        /// This method tokenizes the input and tries to match it to some tokens in the list above 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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