using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_CC
{
    public class SimpleParser
    {
        private List<string> tokenList;
        private List<string> productionList;

        public SimpleParser(List<string> tokens)
        {
            tokenList = tokens;
            productionList = new List<string>();
        }

        public bool Parse()
        {
            while (tokenList.Count > 0 && productionList.Count > 0)
            {
                string currentToken = tokenList[0];

                if (productionList[0] == currentToken)
                {
                    // Current token in production list matches the current token in token list.
                    tokenList.RemoveAt(0);
                    productionList.RemoveAt(0);
                    Console.WriteLine($"Matched token: {currentToken}");
                }
                else
                {
                    // No match, parsing failed.
                    Console.WriteLine($"Parsing failed at token: {currentToken}");
                    return false;
                }
            }

            // Parsing is successful if both lists are empty.
            if (tokenList.Count == 0 && productionList.Count == 0)
            {
                Console.WriteLine("Parsing successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Parsing failed: Lists not empty.");
                return false;
            }
        }

        public void SetProductionList(List<string> production)
        {
            productionList = production;
        }
    }
}
