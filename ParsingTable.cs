using System;
using System.Collections.Generic;

public class ParsingTable
{
    private Dictionary<string, List<List<string>>> productionRules;

    public ParsingTable()
    {
        productionRules = new Dictionary<string, List<List<string>>>();
        // Initialize the production rules here
        AddProduction("RECIPE", new List<List<string>> { new List<string> { "RECIPE", "ID", "DO", "METHOD_MAIN", "DONE" } });
        AddProduction("METHOD_MAIN", new List<List<string>> { new List<string> { "METHOD", "MAIN", "LEFTPARENTHESIS", "RIGHTPARENTHESIS", "DO", "STMNT_BLOCK", "DONE" } });
        AddProduction("STMNT_BLOCK", new List<List<string>> { new List<string> { "STMNT", "STMNT_BLOCK" }, new List<string> { } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "ASK", "LEFTPARENTHESIS", "ID", "RIGHTPARENTHESIS", "SEMICOLON" } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "SPEAK", "LEFTPARENTHESIS", "STRINGLIT", "RIGHTPARENTHESIS", "SEMICOLON" } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "SHARE", "INTEGERLIT", "SEMICOLON" } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "ID", "ASSIGN", "EXPR", "SEMICOLON" } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "ID", "ASSIGN", "ARRAY_ACCESS", "SEMICOLON" } });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "IF", "EXPR", "THEN", "STMNT_BLOCK", "ELSE_BLOCK", "DONE" } });
        AddProduction("ELSE_BLOCK", new List<List<string>> { new List<string> { "ELSE", "STMNT_BLOCK" } });
        AddProduction("ELSE_BLOCK", new List<List<string>> { });
        AddProduction("STMNT", new List<List<string>> { new List<string> { "FUNCTION_CALL", "SEMICOLON" } });
        AddProduction("EXPR", new List<List<string>> { new List<string> { "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "PLUS", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "MINUS", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "AND", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "GREATER", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "GREATEREQUAL", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "LESSER", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "LESSEREQUAL", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "EQUAL", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { new List<string> { "NEQ", "TERM", "EXPR_PRIME" } });
        AddProduction("EXPR_PRIME", new List<List<string>> { });
        AddProduction("TERM", new List<List<string>> { new List<string> { "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "STAR", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "FORWARD_SLASH", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "AND", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "GREATER", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "GREATEREQUAL", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "LESSER", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "LESSEREQUAL", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "EQUAL", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { new List<string> { "NEQ", "FACTOR", "TERM_PRIME" } });
        AddProduction("TERM_PRIME", new List<List<string>> { });
        AddProduction("FACTOR", new List<List<string>> { new List<string> { "ID" } });
        AddProduction("FACTOR", new List<List<string>> { new List<string> { "INTEGERLIT" } });
        AddProduction("FACTOR", new List<List<string>> { new List<string> { "STRINGLIT" } });
        AddProduction("FACTOR", new List<List<string>> { new List<string> { "ARRAY_ACCESS" } });
        AddProduction("FACTOR", new List<List<string>> { new List<string> { "FUNCTION_CALL" } });
        AddProduction("ARRAY_ACCESS", new List<List<string>> { new List<string> { "ID", "LEFTBRACKET", "EXPR", "RIGHTBRACKET" } });
        AddProduction("ARRAY_DECL", new List<List<string>> { new List<string> { "ID", "LEFTBRACKET", "ARRAY_SIZE", "RIGHTBRACKET", "SEMICOLON" } });
        AddProduction("ARRAY_SIZE", new List<List<string>> { new List<string> { "INTEGERLIT" } });
        AddProduction("ARRAY_SIZE", new List<List<string>> { new List<string> { "ID" } });
        AddProduction("ARRAY_ASSIGNMENT", new List<List<string>> { new List<string> { "ID", "LEFTBRACKET", "EXPR", "RIGHTBRACKET", "ASSIGN", "EXPR", "SEMICOLON" } });
        AddProduction("FUNCTION_CALL", new List<List<string>> { new List<string> { "AT_SIGN", "ID", "LEFTPARENTHESIS", "ARGUMENT_LIST", "RIGHTPARENTHESIS" } });
        AddProduction("ARGUMENT_LIST", new List<List<string>> { new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" } });
        AddProduction("ARGUMENT_LIST_PRIME", new List<List<string>> { new List<string> { "COMMA", "EXPR", "ARGUMENT_LIST_PRIME" } });
        AddProduction("ARGUMENT_LIST_PRIME", new List<List<string>> { });
        AddProduction("TYPE", new List<List<string>> { new List<string> { "INTEGER" } });
        AddProduction("TYPE", new List<List<string>> { new List<string> { "FLOAT" } });
        AddProduction("TYPE", new List<List<string>> { new List<string> { "STRING" } });
        AddProduction("TYPE", new List<List<string>> { new List<string> { "BOOLEAN" } });


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
}
