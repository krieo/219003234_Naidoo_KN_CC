using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_CC
{
    public class Parser
    {
        private List<Token> tokens;
        private int currentTokenIndex = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void ParseProgram()
        {
            while (currentTokenIndex < tokens.Count)
            {
                ParseRecipe();
            }
        }

        private void ParseRecipe()
        {
            Match("RECIPE");
            if (IsValidIdentifier(GetCurrentToken().Lexeme))
            {
                Console.WriteLine($"Parsing recipe: {GetCurrentToken().Lexeme}");
                Match("ID"); // This matches the recipe name
            }
            else
            {
                throw new Exception("Recipe name must be a valid identifier.");
            }
            Match("DO");
            ParseStatementBlock();
            Match("DONE");
            Console.WriteLine($"Parsed a recipe");
        }

        private void ParseMethodMain()
        {
            Match("METHOD");
            if (GetCurrentToken().Lexeme == "Main")
            {
                Match("ID"); // This matches the method name
            }
            else
            {
                throw new Exception("Main method must be named 'Main'.");
            }
            Match("LEFTPARENTHESIS");
            Match("RIGHTPARENTHESIS");
            Match("AS");
            Match("INTEGER");
            Match("DO");
            ParseStatementBlock();
            Match("DONE");
            Console.WriteLine($"Parsed the Main method");
        }


        private bool IsValidIdentifier(string lexeme)
        {
            // A valid identifier must start with a letter or underscore and can include letters, digits, and underscores.
            // The lexeme "HelloWorld" is a valid identifier.
            return System.Text.RegularExpressions.Regex.IsMatch(lexeme, "^[a-zA-Z_][a-zA-Z0-9_]*$");
        }


        private bool IsKeyword(string lexeme)
        {
            // Define your list of keywords here
            string[] keywords = new string[]
            {
        "RECIPE", "METHOD", "INTEGER", "INGREDIENT", "FLOAT", "SPEAK", "SHARE", "WHILE", "DONE", "LOOP",
        "ELSE", "ASK", "AS", "DO", "IF", "TRUE", "FALSE", "ARRAY", "ID"
            };

            return keywords.Contains(lexeme);
        }




        private void ParseStatementBlock()
        {
            while (currentTokenIndex < tokens.Count && tokens[currentTokenIndex].Lexeme != "DONE")
            {
                ParseStatement();
            }
        }

        private void ParseStatement()
        {
            string nextTokenLexeme = GetCurrentToken().Lexeme;
            switch (nextTokenLexeme)
            {
                case "ASK":
                    ParseAskStatement();
                    break;
                case "SPEAK":
                    ParseSpeakStatement();
                    break;
                case "SHARE":
                    ParseShareStatement();
                    break;
                case "IF":
                    ParseIfStatement();
                    break;
                case "LOOP":
                    ParseLoopStatement();
                    break;
                case "ID":
                    if (PeekNextToken().Lexeme == "=")
                    {
                        ParseAssignment();
                    }
                    else if (PeekNextToken().Lexeme == "[")
                    {
                        ParseArrayAccess();
                    }
                    else if (PeekNextToken().Lexeme == "@")
                    {
                        ParseFunctionCall();
                    }
                    else
                    {
                        throw new Exception($"Unexpected token: {nextTokenLexeme}");
                    }
                    break;
                default:
                    throw new Exception($"Unexpected token: {nextTokenLexeme}");
            }
        }

        private void ParseAskStatement()
        {
            Match("ASK");
            Match("LEFTPARENTHESIS");
            Match("ID");
            Match("RIGHTPARENTHESIS");
            Match("SEMICOLON");
        }

        private void ParseSpeakStatement()
        {
            Match("SPEAK");
            Match("LEFTPARENTHESIS");
            Match("STRINGLIT");
            Match("RIGHTPARENTHESIS");
            Match("SEMICOLON");
        }

        private void ParseShareStatement()
        {
            Match("SHARE");
            Match("INTEGERLIT");
            Match("SEMICOLON");
        }

        private void ParseAssignment()
        {
            Match("ID");
            Match("ASSIGN");
            ParseExpression();
            Match("SEMICOLON");
        }

        private void ParseArrayAccess()
        {
            Match("ID");
            Match("LEFTBRACKET");
            ParseExpression();
            Match("RIGHTBRACKET");
        }

        private void ParseIfStatement()
        {
            Match("IF");
            ParseExpression();
            Match("THEN");
            ParseStatementBlock();
            ParseElseBlock();
            Match("DONE");
        }

        private void ParseElseBlock()
        {
            if (PeekNextToken().Lexeme == "ELSE")
            {
                Match("ELSE");
                ParseStatementBlock();
            }
        }

        private void ParseLoopStatement()
        {
            Match("LOOP");
            Match("WHILE");
            ParseExpression();
            Match("DO");
            ParseStatementBlock();
            Match("DONE");
        }

        private void ParseFunctionCall()
        {
            Match("@");
            Match("ID");
            Match("LEFTPARENTHESIS");
            if (GetCurrentToken().Lexeme != "RIGHTPARENTHESIS")
            {
                ParseArgumentList();
            }
            Match("RIGHTPARENTHESIS");
            Match("SEMICOLON");
        }

        private void ParseArgumentList()
        {
            ParseExpression();
            while (GetCurrentToken().Lexeme == "COMMA")
            {
                Match("COMMA");
                ParseExpression();
            }
        }

        private void ParseExpression()
        {
            ParseTerm();
            ParseExpressionPrime();
        }

        private void ParseExpressionPrime()
        {
            if (IsOperator(GetCurrentToken().Lexeme))
            {
                Match(GetCurrentToken().Lexeme);
                ParseTerm();
                ParseExpressionPrime();
            }
        }

        private void ParseTerm()
        {
            string nextTokenLexeme = GetCurrentToken().Lexeme;
            if (nextTokenLexeme == "ID" || nextTokenLexeme == "INTEGERLIT" || nextTokenLexeme == "STRINGLIT")
            {
                Match(nextTokenLexeme);
            }
            else if (nextTokenLexeme == "LEFTPARENTHESIS")
            {
                Match("LEFTPARENTHESIS");
                ParseExpression();
                Match("RIGHTPARENTHESIS");
            }
            else
            {
                throw new Exception($"Unexpected token: {nextTokenLexeme}");
            }
        }

        private void Match(string expectedToken)
        {
            Token currentToken = GetCurrentToken();
            if (currentToken.Lexeme == expectedToken)
            {
                currentTokenIndex++;
            }
            else
            {
                throw new Exception($"Expected '{expectedToken}' but found '{currentToken.Lexeme}'");
            }
        }

        private Token GetCurrentToken()
        {
            if (currentTokenIndex < tokens.Count)
            {
                return tokens[currentTokenIndex];
            }
            else
            {
                throw new Exception("Reached end of input");
            }
        }

        private Token PeekNextToken()
        {
            if (currentTokenIndex + 1 < tokens.Count)
            {
                return tokens[currentTokenIndex + 1];
            }
            else
            {
                throw new Exception("Reached end of input");
            }
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" ||
                   token == "==" || token == "!=" || token == "<" || token == "<=" ||
                   token == ">" || token == ">=";
        }
    }
}
