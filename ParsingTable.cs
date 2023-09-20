using System;
using System.Collections.Generic;
using System.Text;

public class ParsingTable
{
    private Dictionary<string, List<List<string>>> productionRules;

    public ParsingTable()
    {
        productionRules = new Dictionary<string, List<List<string>>>();

        // Initialize the production rules for your language here
       
        AddProduction("<program>", new List<List<string>>{new List<string> { "<recipe>" }});
        AddProduction("<recipe>", new List<List<string>> { new List<string> { "RECIPE","ID","DO","<method>","DONE" } });

        AddProduction("<method>", new List<List<string>> { new List<string> { "instantiateMethod", "<method>" } });


        AddProduction("<instantiateMethod>", new List<List<string>> { new List<string> { "METHOD", "ID", "LEFTPARENTHESIS", "LEFTPARENTHESIS", "AS", "<variableMethod>" } });
        AddProduction("<variableMethod>", new List<List<string>> { new List<string> { "INTEGER" } });
        AddProduction("<variableMethod>", new List<List<string>> { new List<string> { "STRING" } });

        AddProduction("<instantiateVariable>", new List<List<string>> { new List<string> { "INGREDIENT","ID","AS","<variable>" } });
        AddProduction("<variable>", new List<List<string>> { new List<string> { "INTEGER" , "SEMICOLON" } });
        AddProduction("<variable>", new List<List<string>> { new List<string> { "STRING", "SEMICOLON" } });


    }

    public void AddProduction(string nonTerminal, List<List<string>> production)
    {
        if (!productionRules.ContainsKey(nonTerminal))
        {
            productionRules[nonTerminal] = new List<List<string>>();
        }

        productionRules[nonTerminal].AddRange(production);
    }

    public List<List<string>> GetProduction(string nonTerminal)
    {
        if (productionRules.ContainsKey(nonTerminal))
        {
            return productionRules[nonTerminal];
        }

        throw new ArgumentException($"Production rule for {nonTerminal} not found.");
    }

    public string GenerateFormattedRules()
    {
        StringBuilder formattedRules = new StringBuilder();

        foreach (var nonTerminal in productionRules.Keys)
        {
            formattedRules.AppendLine($"{nonTerminal} ::=");
            foreach (var production in productionRules[nonTerminal])
            {
                formattedRules.AppendLine($"  {string.Join(" ", production)}");
            }
        }

        return formattedRules.ToString();
    }
}
