using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_CC
{
    public class SimpleParser
    {
        private List<string> tokenList;
        private int currentPosition;
        private ParsingTable parsingTable;

        public SimpleParser(List<string> tokens)
        {
            tokenList = tokens;
            currentPosition = 0; // Initialize the current position
            parsingTable = new ParsingTable(); // Initialize the parsing table
        }

        // Start the parsing process
        public bool ParseProgram()
        {
            return ParseNonTerminal("<program>");
        }

        // Parsing function for non-terminals
        private bool ParseNonTerminal(string nonTerminal)
        {
            // Get the next token
            string currentToken = currentPosition < tokenList.Count ? tokenList[currentPosition] : null;

            // Get the production rules for the current non-terminal
            List<List<string>> productionRules = parsingTable.GetProduction(nonTerminal);

            // Log the current non-terminal and token
            Console.WriteLine($"Parsing {nonTerminal} with current token: {currentToken}");

            // Try each production rule
            foreach (var production in productionRules)
            {
                int originalPosition = currentPosition; // Store the original position

                // Log the current production
                Console.WriteLine($"Trying production: {string.Join(" ", production)}");

                // Try to match the production
                bool productionMatched = true;
                foreach (var symbol in production)
                {
                    if (symbol.StartsWith("<"))
                    {
                        // Symbol is a non-terminal
                        productionMatched = ParseNonTerminal(symbol);
                    }
                    else if (currentToken == symbol)
                    {
                        // Symbol is a terminal and matches the current token
                        currentPosition++; // Consume the token
                    }
                    else
                    {
                        // Symbol is a terminal, but it doesn't match the current token
                        productionMatched = false;
                        break;
                    }
                }

                if (productionMatched)
                {
                    Console.WriteLine("Production matched!");
                    return true; // Successfully matched this production
                }
                else
                {
                    Console.WriteLine("Production not matched. Backtracking...");
                    currentPosition = originalPosition; // Reset the position and try the next production
                }
            }

            Console.WriteLine($"Parsing {nonTerminal} failed.");
            return false; // Parsing failed for this non-terminal
        }
    }
}
