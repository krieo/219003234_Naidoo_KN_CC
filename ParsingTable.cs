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
        AddProduction("<program>", new List<List<string>>
        {
            new List<string> { "<recipe>" }
        });

        AddProduction("<recipe>", new List<List<string>>
        {
            new List<string> { "RECIPE", "<identifier>", "<block>" }
        });
        AddProduction("<block>", new List<List<string>>
{
    new List<string> { "DO", "<statement>", "DONE" }
});


        AddProduction("<identifier>", new List<List<string>>
{
    new List<string> { "ID" }
});


        // Add more production rules here
        AddProduction("<statement>", new List<List<string>>
{
    new List<string> { "<variable-declaration>" },
    new List<string> { "<assignment>" },
    new List<string> { "<conditional-statement>" },
    new List<string> { "<loop>" },
    new List<string> { "<function-call>" },
    new List<string> { "<input>" },
    new List<string> { "<output>" },
    new List<string> { "<return>" }
});

        AddProduction("<variable-declaration>", new List<List<string>>
{
    new List<string> { "INGREDIENT", "<identifier>", "AS", "<type>", ";" }
});

        AddProduction("<assignment>", new List<List<string>>
{
    new List<string> { "<identifier>", "=", "<expression>", ";" }
});

        AddProduction("<conditional-statement>", new List<List<string>>
{
    new List<string> { "IF", "<expression>", "THEN", "<block>", "ELSE", "<block>", "DONE" },
    new List<string> { "IF", "<expression>", "THEN", "<block>", "DONE" }
});

        AddProduction("<loop>", new List<List<string>>
{
    new List<string> { "LOOP", "WHILE", "<expression>", "DO", "<block>", "DONE" }
});

        AddProduction("<function-call>", new List<List<string>>
{
    new List<string> { "@", "<identifier>", "(", "<expression-list>", ")", ";" }
});

        AddProduction("<input>", new List<List<string>>
{
    new List<string> { "ASK", "(", "<identifier>", ")", ";" }
});

        AddProduction("<output>", new List<List<string>>
{
    new List<string> { "SPEAK", "(", "<expression>", ")", ";" }
});

        AddProduction("<return>", new List<List<string>>
{
    new List<string> { "SHARE", "<expression>", ";" }
});

        AddProduction("<expression-list>", new List<List<string>>
{
    new List<string> { "<expression>", ",", "<expression-list>" },
    new List<string> { "<expression>" }
});


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
