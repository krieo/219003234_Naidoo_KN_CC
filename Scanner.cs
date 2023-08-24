using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _219003234_Naidoo_KN_CC
{
    /// <summary>
    /// This class is the scanner for the language YOU ARE BUSY HERE WHAT YOU ARE DOING IS SEPERATING THE MAIN CLASSES FROM THE PROGRAM TO THEIR OWN CLASS
    /// </summary>
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

}
