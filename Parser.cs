﻿using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_CC
{
    public class Parser
    {
        private List<string> inputTokens;
        private Stack<string> stack;
        private ParsingTable parsingTable;

        // The root of the AST
        public ASTProgramNode AST { get; private set; }

        // Tracks the current method being parsed (for adding statements)
        private ASTMethodNode currentMethod;

        public Parser(List<Token> tokens)
        {
            inputTokens = new List<string>();
            foreach (Token token in tokens)
            {
                inputTokens.Add(token.Type.ToString());
            }

            stack = new Stack<string>();
            parsingTable = new ParsingTable();
            stack.Push("RECIPE"); // Push the start symbol onto the stack
            AST = new ASTProgramNode(); // Initialize the AST root
        }

        public Parser(List<string> tokenTypes)
        {
            inputTokens = tokenTypes;
            stack = new Stack<string>();
            parsingTable = new ParsingTable();
            stack.Push("RECIPE"); // Push the start symbol onto the stack
            AST = new ASTProgramNode(); // Initialize the AST root
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

                        // Construct AST nodes while parsing
                        ConstructASTNode(topOfStack, productionRule);
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
            return symbol == "RECIPE" || symbol == "METHOD" || symbol == "METHOD_MAIN"
                || symbol == "STMNT_BLOCK" || symbol == "STMNT" || symbol == "ELSE_BLOCK"
                || symbol == "EXPR" || symbol == "EXPR_PRIME" || symbol == "TERM"
                || symbol == "TERM_PRIME" || symbol == "FACTOR" || symbol == "ARRAY_ACCESS"
                || symbol == "FUNCTION_CALL" || symbol == "ARGUMENT_LIST" || symbol == "ARGUMENT_LIST_PRIME"
                || symbol == "TYPE" || symbol == "LOOP";
        }

        private bool IsTerminal(string symbol)
        {
            // Implement a check to determine if the symbol is a terminal
            return symbol == "RECIPE" || symbol == "ID" || symbol == "DO" || symbol == "DONE"
                || symbol == "METHOD" || symbol == "AS" || symbol == "INTEGER" || symbol == "INGREDIENT"
                || symbol == "STRING" || symbol == "SEMICOLON" || symbol == "ASK" || symbol == "LEFTPARENTHESIS"
                || symbol == "RIGHTPARENTHESIS" || symbol == "STRINGLIT" || symbol == "SPEAK"
                || symbol == "SHARE" || symbol == "INTEGERLIT" || symbol == "PLUS" || symbol == "MINUS"
                || symbol == "AND" || symbol == "GREATER" || symbol == "EQUAL";
        }
        private void ConstructASTNode(string nonTerminal, List<string> productionRule)
        {
            switch (nonTerminal)
            {
                case "RECIPE":
                    // Construct the root of the AST (program node)
                    AST = new ASTProgramNode();
                    break;

                case "METHOD":
                    // Construct an ASTMethodNode
                    ASTMethodNode method = new ASTMethodNode();
                    method.MethodName = productionRule[1]; // Assuming the method name is the second token in the production rule
                    currentMethod = method; // Set the current method to the one being constructed
                    AST.AddMethod(method); // Add the method to the program node
                    break;

                case "METHOD_MAIN":
                    // Construct an ASTMethodNode for the main method
                    ASTMethodNode mainMethod = new ASTMethodNode();
                    mainMethod.MethodName = "MAIN"; // Assuming the method name is "MAIN" for the main method
                    currentMethod = mainMethod; // Set the current method to the main method
                    AST.AddMethod(mainMethod); // Add the main method to the program node
                    break;

                case "STMNT_BLOCK":
                    // Handle constructing AST nodes for STMNT_BLOCK (list of statements)
                    // Iterate through the productionRule to construct individual statements
                    for (int i = 0; i < productionRule.Count; i++)
                    {
                        string token = productionRule[i];
                        if (token == "STMNT")
                        {
                            // Construct an ASTStatementNode for the statement
                            ASTStatementNode statement = new ASTStatementNode();
                            // Add the statement to the current method's Statements list
                            currentMethod.AddChild(statement);
                        }
                        // Handle other tokens within STMNT_BLOCK if necessary
                    }
                    break;

                case "STMNT":
                    // Handle constructing AST nodes for STMNT (individual statements)
                    ASTStatementNode statementNode = new ASTStatementNode();
                    currentMethod.AddChild(statementNode);
                    break;

                case "ELSE_BLOCK":
                    // Handle constructing AST nodes for ELSE_BLOCK (else block of an if statement)
                    for (int i = 0; i < productionRule.Count; i++)
                    {
                        string token = productionRule[i];
                        if (token == "STMNT")
                        {
                            ASTStatementNode elseStatement = new ASTStatementNode();
                            currentMethod.AddChild(elseStatement);
                        }
                        // Handle other tokens within ELSE_BLOCK if necessary
                    }
                    break;

                case "EXPR":
                    // Handle constructing AST nodes for EXPR
                    ASTExpressionNode expression = new ASTExpressionNode("expression"); // Replace with appropriate expression type
                    currentMethod.AddChild(expression);
                    break;

                case "EXPR_PRIME":
                    // Handle constructing AST nodes for EXPR_PRIME
                    string expressionPrimeOperator = productionRule[0]; // Replace with the correct way to obtain the operator
                    ASTTermNode expressionPrimeRightTerm = new ASTTermNode(); // Replace with the correct way to obtain the right term
                    ASTExpressionPrimeNode expressionPrime = new ASTExpressionPrimeNode(expressionPrimeOperator, expressionPrimeRightTerm);
                    currentMethod.AddChild(expressionPrime);
                    break;

                case "TERM":
                    // Handle constructing AST nodes for TERM
                    ASTTermNode term = new ASTTermNode();
                    currentMethod.AddChild(term);
                    break;

                case "TERM_PRIME":
                    // Handle constructing AST nodes for TERM_PRIME
                    string termPrimeOperator = productionRule[0]; // Replace with the correct way to obtain the operator
                    ASTTermNode termPrimeRightTerm = new ASTTermNode(); // Replace with the correct way to obtain the right term
                    ASTTermPrimeNode termPrime = new ASTTermPrimeNode(termPrimeOperator, termPrimeRightTerm);
                    currentMethod.AddChild(termPrime);
                    break;

                case "FACTOR":
                    // Handle constructing AST nodes for FACTOR
                    ASTFactorNode factor = new ASTFactorNode(productionRule[0]); // Assuming the factor value is the first token in the production rule
                    currentMethod.AddChild(factor);
                    break;

                case "ARRAY_ACCESS":
                    // Handle constructing AST nodes for ARRAY_ACCESS
                    ASTIdentifierNode arrayIdentifier = new ASTIdentifierNode(productionRule[0]); // Assuming the identifier is the first token in the production rule
                    ASTExpressionNode arrayIndex = currentMethod.GetLastExpression(); // Assuming you have a method to get the last expression
                    ASTArrayAccessNode arrayAccess = new ASTArrayAccessNode(arrayIdentifier, arrayIndex);
                    currentMethod.AddChild(arrayAccess);
                    break;

                case "FUNCTION_CALL":
                    // Handle constructing AST nodes for FUNCTION_CALL
                    string functionName = productionRule[1]; // Assuming the function name is the second token in the production rule
                    ASTFunctionCallNode functionCall = new ASTFunctionCallNode(functionName);
                    currentMethod.AddChild(functionCall);
                    break;

                case "ARGUMENT_LIST":
                    // Handle constructing AST nodes for ARGUMENT_LIST
                    ASTArgumentListNode argumentList = new ASTArgumentListNode(); // You can provide the appropriate parameters here
                    currentMethod.AddChild(argumentList);
                    break;

                case "TYPE":
                    // Handle constructing AST nodes for TYPE
                    string typeName = productionRule[0]; // Assuming the type name is the first token in the production rule
                    ASTTypeNode type = new ASTTypeNode(typeName);
                    currentMethod.AddChild(type);
                    break;

                case "LOOP":
                    // Handle constructing AST nodes for LOOP
                    ASTLoopNode loop = new ASTLoopNode(); // You can provide the appropriate parameters here
                    currentMethod.AddChild(loop);
                    break;

                // Handle other non-terminals in a similar manner

                default:
                    // Handle unknown non-terminals (optional)
                    break;
            }
        }


    }
}
