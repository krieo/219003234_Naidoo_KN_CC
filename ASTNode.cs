using System.Collections.Generic;

namespace _219003234_Naidoo_KN_CC
{
    // Base class for all AST nodes
    public abstract class ASTNode
    {
        // You can add common properties or methods shared among all AST nodes here
        public int LineNumber { get; set; } // Store line number information
        public int ColumnNumber { get; set; } // Store column number information

    }

    // AST node for a program
    public class ASTProgramNode : ASTNode
    {
        public List<ASTMethodNode> Methods { get; } = new List<ASTMethodNode>();

        public void AddMethod(ASTMethodNode method)
        {
            Methods.Add(method);
        }
    }

    // AST node for a method
    public class ASTMethodNode : ASTNode
    {
        public string MethodName { get; set; }
        public List<ASTStatementNode> Statements { get; } = new List<ASTStatementNode>();

        public void AddStatement(ASTStatementNode statement)
        {
            Statements.Add(statement);
        }
    }

    // AST node for a statement
    public class ASTStatementNode : ASTNode
    {
        public List<ASTNode> Children { get; } = new List<ASTNode>();

        public void AddChild(ASTNode child)
        {
            Children.Add(child);
        }
    }

    // AST node for an expression (you can create more specialized expression nodes)
    public class ASTExpressionNode : ASTNode
    {
        // Property to store the type of expression (e.g., arithmetic, logical, etc.)
        public string ExpressionType { get; set; }

        // Property to store the value of the expression if applicable
        public string Value { get; set; }

        // Property to represent the left operand of the expression
        public ASTExpressionNode LeftOperand { get; set; }

        // Property to represent the operator (e.g., +, -, *, /, etc.)
        public string Operator { get; set; }

        // Property to represent the right operand of the expression
        public ASTExpressionNode RightOperand { get; set; }

        // Constructor to initialize an expression node with a specific type
        public ASTExpressionNode(string expressionType)
        {
            ExpressionType = expressionType;
        }

        // Constructor for binary operations (e.g., +, -, *, /, etc.)
        public ASTExpressionNode(ASTExpressionNode leftOperand, string @operator, ASTExpressionNode rightOperand)
        {
            LeftOperand = leftOperand;
            Operator = @operator;
            RightOperand = rightOperand;
        }
    }

    // AST node for an if statement
    public class ASTIfStatementNode : ASTStatementNode
    {
        public ASTExpressionNode Condition { get; set; }
        public List<ASTStatementNode> TrueStatements { get; } = new List<ASTStatementNode>();
        public List<ASTStatementNode> FalseStatements { get; } = new List<ASTStatementNode>();

        public void AddTrueStatement(ASTStatementNode statement)
        {
            TrueStatements.Add(statement);
        }

        public void AddFalseStatement(ASTStatementNode statement)
        {
            FalseStatements.Add(statement);
        }
    }

    // AST node for a while loop
    public class ASTWhileLoopNode : ASTStatementNode
    {
        public ASTExpressionNode Condition { get; set; }
        public List<ASTStatementNode> LoopStatements { get; } = new List<ASTStatementNode>();

        public void AddLoopStatement(ASTStatementNode statement)
        {
            LoopStatements.Add(statement);
        }
    }

    // AST node for an input statement
    public class ASTInputStatementNode : ASTStatementNode
    {
        public string InputVariable { get; set; }
        public string Message { get; set; }
    }

    // AST node for an output statement
    public class ASTOutputStatementNode : ASTStatementNode
    {
        public string OutputExpression { get; set; }
        public string Message { get; set; }
    }


}
