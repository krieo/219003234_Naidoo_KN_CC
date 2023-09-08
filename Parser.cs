using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_CC
{
    public class Parser
    {
        private List<string> inputTokens;
        private Stack<string> stack;
        private ParsingTable parsingTable;

        public Parser(List<Token> tokens)
        {
            inputTokens = new List<string>(); // Initialize the list of token types
            foreach (Token token in tokens)
            {
                inputTokens.Add(token.Type); // Add the token type instead of lexeme
            }

            stack = new Stack<string>();
            parsingTable = new ParsingTable();
            stack.Push("RECIPE"); // Push the start symbol onto the stack
        }

        public Parser(List<string> tokenTypes)
        {
            inputTokens = tokenTypes;
            stack = new Stack<string>();
            parsingTable = new ParsingTable();
            stack.Push("RECIPE"); // Push the start symbol onto the stack
        }


        public void Parse()
        {
            while (stack.Count > 0)
            {
                string currentToken = inputTokens[0];
                string topOfStack = stack.Peek();

                if (IsTerminal(topOfStack))
                {
                    if (topOfStack == currentToken)
                    {
                        stack.Pop();
                        inputTokens.RemoveAt(0);
                    }
                    else
                    {
                        // Handle mismatched terminal symbol error
                        throw new Exception($"Parsing error: Expected '{topOfStack}', but found '{currentToken}'.");
                    }
                }
                else if (IsNonTerminal(topOfStack))
                {
                    List<string> productionRule = parsingTable.GetProductionRules(topOfStack, currentToken);

                    if (productionRule != null)
                    {
                        stack.Pop();

                        // Push the right-hand side of the production rule onto the stack in reverse order
                        for (int i = productionRule.Count - 1; i >= 0; i--)
                        {
                            stack.Push(productionRule[i]);
                        }
                    }
                    else
                    {
                        // Handle no valid production rule found error
                        throw new Exception($"Parsing error: No valid production rule for ({topOfStack}, {currentToken}).");
                    }
                }
                else
                {
                    // Handle other symbols (e.g., epsilon, special symbols)
                    stack.Pop();
                }
            }

            // At this point, the parsing is successful
            Console.WriteLine("Parsing successful.");
        }

        private bool IsNonTerminal(string symbol)
        {
            // Implement a check to determine if the symbol is a non-terminal
            return symbol == "RECIPE" || symbol == "METHOD" || symbol == "STMNT_BLOCK"
                || symbol == "STMNT" || symbol == "ELSE_BLOCK" || symbol == "EXPR"
                || symbol == "EXPR_PRIME" || symbol == "TERM" || symbol == "TERM_PRIME"
                || symbol == "FACTOR" || symbol == "ARRAY_ACCESS" || symbol == "FUNCTION_CALL"
                || symbol == "ARGUMENT_LIST" || symbol == "ARGUMENT_LIST_PRIME"
                || symbol == "TYPE";
        }

        private bool IsTerminal(string symbol)
        {
            // Implement a check to determine if the symbol is a terminal
            return symbol == "RECIPE" || symbol == "ID" || symbol == "DO" || symbol == "DONE"
                || symbol == "METHOD" || symbol == "AS" || symbol == "INTEGER" || symbol == "INGREDIENT"
                || symbol == "STRING" || symbol == "SEMICOLON" || symbol == "ASK" || symbol == "LEFTPARENTHESIS"
                || symbol == "RIGHTPARENTHESIS" || symbol == "STRINGLIT" || symbol == "SPEAK"
                || symbol == "SHARE" || symbol == "INTEGERLIT" || symbol == "PLUS" || symbol == "MINUS"
                || symbol == "AND" || symbol == "GREATER" || symbol == "GREATEREQUAL" || symbol == "LESSER"
                || symbol == "LESSEREQUAL" || symbol == "EQUAL" || symbol == "NEQ" || symbol == "STAR"
                || symbol == "FORWARD_SLASH" || symbol == "THEN" || symbol == "ELSE" || symbol == "COMMA"
                || symbol == "ASSIGN" || symbol == "LEFTBRACKET" || symbol == "RIGHTBRACKET" || symbol == "AT_SIGN";
        }

    }
}
