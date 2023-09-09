using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_CC
{
    public class ParsingTable
    {
        // Initialize the parsing table as a dictionary
        private Dictionary<(string NonTerminal, string Terminal), List<string>> parsingTable;

        public ParsingTable()
        {
            parsingTable = new Dictionary<(string, string), List<string>>();

            // Populate the parsing table with production rules
            parsingTable[("RECIPE", "RECIPE")] = new List<string> { "RECIPE", "ID", "DO", "METHOD_MAIN", "DONE" };
            parsingTable[("METHOD_MAIN", "METHOD")] = new List<string> { "METHOD", "MAIN", "LEFTPARENTHESIS", "RIGHTPARENTHESIS", "DO", "STMNT_BLOCK", "DONE" };
            parsingTable[("STMNT_BLOCK", "ASK")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "SPEAK")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "SHARE")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "ID")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "IF")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "FUNCTION_CALL")] = new List<string> { "STMNT", "STMNT_BLOCK" };
            parsingTable[("STMNT_BLOCK", "DONE")] = new List<string> { "ε" };

            parsingTable[("STMNT", "ASK")] = new List<string> { "ASK", "LEFTPARENTHESIS", "ID", "RIGHTPARENTHESIS", "SEMICOLON" };
            parsingTable[("STMNT", "SPEAK")] = new List<string> { "SPEAK", "LEFTPARENTHESIS", "STRINGLIT", "RIGHTPARENTHESIS", "SEMICOLON" };
            parsingTable[("STMNT", "SHARE")] = new List<string> { "SHARE", "INTEGERLIT", "SEMICOLON" };
            parsingTable[("STMNT", "ID")] = new List<string> { "ID", "ASSIGN", "EXPR", "SEMICOLON" };
            parsingTable[("STMNT", "IF")] = new List<string> { "IF", "EXPR", "THEN", "STMNT_BLOCK", "ELSE_BLOCK", "DONE" };
            parsingTable[("STMNT", "FUNCTION_CALL")] = new List<string> { "FUNCTION_CALL", "SEMICOLON" };

            parsingTable[("ELSE_BLOCK", "ELSE")] = new List<string> { "ELSE", "STMNT_BLOCK" };
            parsingTable[("ELSE_BLOCK", "DONE")] = new List<string> { "ε" };

            parsingTable[("EXPR", "ID")] = new List<string> { "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR", "INTEGERLIT")] = new List<string> { "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR", "STRINGLIT")] = new List<string> { "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR", "LEFTPARENTHESIS")] = new List<string> { "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR", "AT_SIGN")] = new List<string> { "TERM", "EXPR_PRIME" };

            parsingTable[("EXPR_PRIME", "PLUS")] = new List<string> { "PLUS", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "MINUS")] = new List<string> { "MINUS", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "AND")] = new List<string> { "AND", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "GREATER")] = new List<string> { "GREATER", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "GREATEREQUAL")] = new List<string> { "GREATEREQUAL", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "LESSER")] = new List<string> { "LESSER", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "LESSEREQUAL")] = new List<string> { "LESSEREQUAL", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "EQUAL")] = new List<string> { "EQUAL", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "NEQ")] = new List<string> { "NEQ", "TERM", "EXPR_PRIME" };
            parsingTable[("EXPR_PRIME", "THEN")] = new List<string> { "ε" };
            parsingTable[("EXPR_PRIME", "SEMICOLON")] = new List<string> { "ε" };

            parsingTable[("TERM", "ID")] = new List<string> { "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM", "INTEGERLIT")] = new List<string> { "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM", "STRINGLIT")] = new List<string> { "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM", "LEFTPARENTHESIS")] = new List<string> { "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM", "AT_SIGN")] = new List<string> { "FACTOR", "TERM_PRIME" };

            parsingTable[("TERM_PRIME", "PLUS")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "MINUS")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "AND")] = new List<string> { "AND", "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM_PRIME", "GREATER")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "GREATEREQUAL")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "LESSER")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "LESSEREQUAL")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "EQUAL")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "NEQ")] = new List<string> { "ε" };
            parsingTable[("TERM_PRIME", "STAR")] = new List<string> { "STAR", "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM_PRIME", "FORWARD_SLASH")] = new List<string> { "FORWARD_SLASH", "FACTOR", "TERM_PRIME" };
            parsingTable[("TERM_PRIME", "SEMICOLON")] = new List<string> { "ε" };

            parsingTable[("FACTOR", "ID")] = new List<string> { "ID" };
            parsingTable[("FACTOR", "INTEGERLIT")] = new List<string> { "INTEGERLIT" };
            parsingTable[("FACTOR", "STRINGLIT")] = new List<string> { "STRINGLIT" };
            parsingTable[("FACTOR", "LEFTPARENTHESIS")] = new List<string> { "LEFTPARENTHESIS", "EXPR", "RIGHTPARENTHESIS" };
            parsingTable[("FACTOR", "AT_SIGN")] = new List<string> { "FUNCTION_CALL" };

            parsingTable[("ARRAY_ACCESS", "ID")] = new List<string> { "ID", "LEFTBRACKET", "EXPR", "RIGHTBRACKET" };

            parsingTable[("FUNCTION_CALL", "AT_SIGN")] = new List<string> { "AT_SIGN", "ID", "LEFTPARENTHESIS", "ARGUMENT_LIST", "RIGHTPARENTHESIS" };

            parsingTable[("ARGUMENT_LIST", "ID")] = new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST", "INTEGERLIT")] = new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST", "STRINGLIT")] = new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST", "LEFTPARENTHESIS")] = new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST", "AT_SIGN")] = new List<string> { "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST", "RIGHTPARENTHESIS")] = new List<string> { "ε" };

            parsingTable[("ARGUMENT_LIST_PRIME", "COMMA")] = new List<string> { "COMMA", "EXPR", "ARGUMENT_LIST_PRIME" };
            parsingTable[("ARGUMENT_LIST_PRIME", "RIGHTPARENTHESIS")] = new List<string> { "ε" };

            parsingTable[("TYPE", "INTEGER")] = new List<string> { "INTEGER" };
            parsingTable[("TYPE", "FLOAT")] = new List<string> { "FLOAT" };
            parsingTable[("TYPE", "STRING")] = new List<string> { "STRING" };
            parsingTable[("TYPE", "BOOLEAN")] = new List<string> { "BOOLEAN" };

        }

        public List<string> GetProductionRules(string nonTerminal, string terminal)
        {
            // Retrieve the production rules for the given (Non-Terminal, Terminal) pair
            if (parsingTable.TryGetValue((nonTerminal, terminal), out List<string> rules))
            {
                return rules;
            }
            else
            {
                // Handle errors if the pair is not in the parsing table
                throw new Exception($"Parsing error: No production rules for ({nonTerminal}, {terminal}).");
            }
        }
    }
}
