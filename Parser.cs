using _219003234_Naidoo_KN_CC;
using System;
using System.Collections.Generic;

public class Parser
{
    private List<string> inputTokens; // List of token types (strings)
    private int currentTokenIndex;
    private ParsingTable parsingTable;

    public Parser(List<string> inputTokens, ParsingTable parsingTable)
    {
        this.inputTokens = inputTokens;
        currentTokenIndex = 0;
        this.parsingTable = parsingTable;
    }

    public void Parse()
    {
        // Start parsing from the initial non-terminal symbol, in this case, "RECIPE"
        ParseNonTerminal("RECIPE");
    }

    private void ParseNonTerminal(string nonTerminal)
    {
        Console.WriteLine($"Parsing {nonTerminal}");
        var production = parsingTable.GetProduction(nonTerminal);

        foreach (var option in production)
        {
            int initialTokenIndex = currentTokenIndex;
            bool optionMatched = true;

            foreach (var symbol in option)
            {
                if (parsingTable.IsTerminal(symbol))
                {
                    // If it's a terminal, match the current input token type
                    if (currentTokenIndex < inputTokens.Count && inputTokens[currentTokenIndex] == symbol)
                    {
                        Console.WriteLine($"Matched: {symbol}");
                        currentTokenIndex++;
                    }
                    else
                    {
                        optionMatched = false;
                        break; // Try the next option
                    }
                }
                else
                {
                    // If it's a non-terminal, recursively parse it
                    ParseNonTerminal(symbol);
                }
            }

            if (optionMatched)
            {
                Console.WriteLine($"Successfully parsed {nonTerminal}");
                return;
            }
            else
            {
                // Reset token index and try the next option
                currentTokenIndex = initialTokenIndex;
            }
        }

        // If no option matched, raise an error
        Console.WriteLine($"Error: Unable to parse {nonTerminal}");
    }
}
