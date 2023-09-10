using System;
using System.Collections.Generic;

public class SimpleParser
{
    private ParsingTable parsingTable;
    private Stack<string> stack;
    private List<string> inputTokens;

    public SimpleParser(List<string> tokens, ParsingTable table)
    {
        inputTokens = tokens;
        parsingTable = table;
    }

    public bool Parse()
    {
        stack = new Stack<string>();

        // Push the start symbol onto the stack
        stack.Push("RECIPE");

        int inputIndex = 0;

        while (stack.Count > 0)
        {
            string currentSymbol = stack.Peek();
            Console.WriteLine($"Current Symbol: {currentSymbol}");

            if (parsingTable.IsTerminal(currentSymbol))
            {
                // If the current symbol is a terminal, compare it with the next input token
                if (currentSymbol == inputTokens[inputIndex])
                {
                    Console.WriteLine($"Matched Terminal: {currentSymbol}");
                    stack.Pop();
                    inputIndex++;
                }
                else
                {
                    // Error: Mismatched terminal
                    Console.WriteLine($"Error: Expected '{currentSymbol}' but got '{inputTokens[inputIndex]}'");
                    return false;
                }
            }
            else if (parsingTable.IsNonTerminal(currentSymbol))
            {
                // Lookup the production rule for the non-terminal
                List<List<string>> production = parsingTable.GetProduction(currentSymbol);

                if (production != null)
                {
                    Console.WriteLine($"Using Production for {currentSymbol}: {string.Join(" | ", production[0])}");

                    // Pop the non-terminal from the stack
                    stack.Pop();

                    // Push the production onto the stack in reverse order
                    for (int i = production.Count - 1; i >= 0; i--)
                    {
                        List<string> productionOption = production[i];
                        foreach (string symbol in productionOption)
                        {
                            stack.Push(symbol);
                        }
                    }
                }
                else
                {
                    // Error: No production rule for the non-terminal
                    Console.WriteLine($"Error: No production rule for '{currentSymbol}'");
                    return false;
                }
            }
            else
            {
                // Error: Invalid symbol
                Console.WriteLine($"Error: Invalid symbol '{currentSymbol}'");
                return false;
            }
        }

        // If the stack is empty and all input tokens have been consumed, parsing is successful
        if (inputIndex == inputTokens.Count)
        {
            Console.WriteLine("Parsing Successful");
            return true;
        }
        else
        {
            // Error: Not all input tokens consumed
            Console.WriteLine($"Error: Not all input tokens consumed. Expected end of input.");
            return false;
        }
    }
}
