using System;
using System.Collections.Generic;

public class Parser
{
    private ParsingTable parsingTable;
    private List<string> inputTokens;
    private List<string> outputList;

    public Parser(ParsingTable table, List<string> tokens)
    {
        parsingTable = table;
        inputTokens = tokens;
        outputList = new List<string>();
    }

    public List<string> Parse()
    {
        ParseNonTerminal("RECIPE");
        return outputList;
    }

    private void ParseNonTerminal(string nonTerminal)
    {
        if (inputTokens.Count > 0)
        {
            string currentToken = inputTokens[0];

            if (parsingTable.IsTerminal(nonTerminal))
            {
                // Non-terminal is a terminal symbol, check if it matches the current token.
                if (currentToken == nonTerminal)
                {
                    outputList.Add(currentToken);
                    inputTokens.RemoveAt(0);
                }
                else
                {
                    throw new InvalidOperationException($"Expected '{nonTerminal}' but found '{currentToken}'");
                }
            }
            else if (parsingTable.IsNonTerminal(nonTerminal))
            {
                // Non-terminal is a non-terminal symbol, choose a production rule.
                var production = parsingTable.GetProduction(nonTerminal);

                foreach (var option in production)
                {
                    bool match = true;
                    List<string> tempOutput = new List<string>();

                    foreach (var symbol in option)
                    {
                        if (parsingTable.IsTerminal(symbol))
                        {
                            // Terminal symbol, try to match with input token.
                            if (currentToken == symbol)
                            {
                                tempOutput.Add(currentToken);
                                inputTokens.RemoveAt(0);
                            }
                            else
                            {
                                match = false;
                                break; // Production doesn't match, try the next one.
                            }
                        }
                        else
                        {
                            // Non-terminal symbol, recursively parse.
                            ParseNonTerminal(symbol);
                            tempOutput.Add(symbol); // Add non-terminal to output.
                        }
                    }

                    if (match)
                    {
                        outputList.AddRange(tempOutput);
                        return; // Successfully matched production, exit.
                    }
                }

                // If no production matched, report an error.
                throw new InvalidOperationException($"No valid production found for '{nonTerminal}'");
            }
            else
            {
                throw new InvalidOperationException($"Invalid symbol: '{nonTerminal}'");
            }
        }
        else
        {
            throw new InvalidOperationException("Unexpected end of input.");
        }
    }
}
