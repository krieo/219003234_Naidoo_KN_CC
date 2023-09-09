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

    // AST node for a binary operation within an expression (e.g., +, -, *, /, etc.)
    public class ASTBinaryOperationNode : ASTExpressionNode
    {
        public ASTBinaryOperationNode(ASTExpressionNode leftOperand, string @operator, ASTExpressionNode rightOperand)
            : base("binary")
        {
            LeftOperand = leftOperand;
            Operator = @operator;
            RightOperand = rightOperand;
        }
    }

    // AST node for literal values (e.g., integers, strings, booleans)
    public class ASTLiteralNode : ASTExpressionNode
    {
        public ASTLiteralNode(string value)
            : base("literal")
        {
            Value = value;
        }
    }
    public class ASTIdentifierNode : ASTExpressionNode
    {
        public string IdentifierName { get; set; }

        public ASTIdentifierNode(string name)
            : base("ID")
        {
            IdentifierName = name;
        }
    }

    // AST node for a function call expression
    public class ASTFunctionCallNode : ASTExpressionNode
    {
        public string FunctionName { get; set; }
        public List<ASTArgumentNode> Arguments { get; } = new List<ASTArgumentNode>();

        public ASTFunctionCallNode(string functionName)
            : base("FUNCTION_CALL")
        {
            FunctionName = functionName;
        }

        public void AddArgument(ASTArgumentNode argument)
        {
            Arguments.Add(argument);
        }
    }

    // AST node for an argument passed to a function in a function call
    public class ASTArgumentNode : ASTNode
    {
        public ASTExpressionNode Value { get; set; }

        public ASTArgumentNode(ASTExpressionNode value)
        {
            Value = value;
        }
    }

    // AST node for data types
    public class ASTTypeNode : ASTNode
    {
        public string TypeName { get; set; }

        public ASTTypeNode(string typeName)
        {
            TypeName = typeName;
        }
    }


    // AST node for array literal
    public class ASTArrayLiteralNode : ASTExpressionNode
    {
        public List<ASTExpressionNode> Elements { get; } = new List<ASTExpressionNode>();

        public ASTArrayLiteralNode()
            : base("array_literal")
        {
        }

        public void AddElement(ASTExpressionNode element)
        {
            Elements.Add(element);
        }
    }

    // AST node for variable declaration
    public class ASTVariableDeclarationNode : ASTStatementNode
    {
        public string VariableName { get; set; }
        public ASTTypeNode VariableType { get; set; }
        public ASTExpressionNode InitialValue { get; set; } // Optional

        public ASTVariableDeclarationNode(string variableName, ASTTypeNode variableType, ASTExpressionNode initialValue = null)
        {
            VariableName = variableName;
            VariableType = variableType;
            InitialValue = initialValue;
        }
    }
    // AST node for array declaration
    public class ASTArrayDeclarationNode : ASTStatementNode
    {
        public string ArrayName { get; set; }
        public ASTTypeNode ElementType { get; set; }

        public ASTArrayDeclarationNode(string arrayName, ASTTypeNode elementType)
        {
            ArrayName = arrayName;
            ElementType = elementType;
        }
    }

    // AST node for array access
    public class ASTArrayAccessNode : ASTExpressionNode
    {
        public ASTIdentifierNode ArrayIdentifier { get; set; }
        public ASTExpressionNode Index { get; set; }

        public ASTArrayAccessNode(ASTIdentifierNode arrayIdentifier, ASTExpressionNode index)
            : base("ARRAY_ACCESS")
        {
            ArrayIdentifier = arrayIdentifier;
            Index = index;
        }
    }



}

/*ASTProgramNode: Represents the entire program and can contain a list of methods or other top-level declarations.

ASTMethodNode: Represents a method or function definition. It contains the method name, parameters, and a block of statements.

ASTStatementNode: Represents a statement in your language. This can include assignment statements, conditional statements (if-else), loops (while, for), input/output statements, and function calls.

ASTAssignmentNode: Represents an assignment statement, including the identifier being assigned and the expression being assigned to it.

ASTIfStatementNode: Represents an if statement, including the condition, the block of statements executed if the condition is true, and an optional else block.

ASTWhileLoopNode: Represents a while loop, including the loop condition and the block of statements that make up the loop body.

ASTInputStatementNode: Represents an input statement, such as reading a value from the user or a file.

ASTOutputStatementNode: Represents an output statement, such as printing a value to the console or a file.



ASTExpressionNode: Represents an expression, which can be an arithmetic expression, a boolean expression, or a function call.

ASTBinaryOperationNode: Represents a binary operation within an expression, like addition, subtraction, multiplication, etc.

ASTLiteralNode: Represents literal values such as integers, strings, or boolean literals.



ASTIdentifierNode: Represents an identifier, which could be a variable name or a function name.

ASTFunctionCallNode: Represents a function call expression, including the function name and its arguments.

ASTArgumentNode: Represents an argument passed to a function in a function call.



ASTTypeNode: Represents data types in your language, such as integers, strings, floats, or custom-defined types.

ASTArrayAccessNode: Represents access to elements in an array, including the array variable and the index.

ASTArrayLiteralNode: Represents an array literal, initializing an array with values.*/