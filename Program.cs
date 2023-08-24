using _219003234_Naidoo_KN_CC;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


class Program
{
    static string ReadTextFromFile(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);
            return content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            return null;
        }
    }

    static void Main(string[] args)
    {
        // string input = "RECIPE HelloWorld DO METHOD Main() AS INTEGER DO SPEAK(\"Hello, World!\"); DONE DONE";
        /*
          string input = @"RECIPE MathOperations
                      DO
                      METHOD Main() AS INTEGER
                      DO
                      INGREDIENT a AS INTEGER;
                      INGREDIENT b AS INTEGER;
                      a = 14;
                      b = 3;
                      INGREDIENT sum AS INTEGER;
                      sum = a + b;
                      INGREDIENT product AS INTEGER;
                      product = a * b;
                      INGREDIENT quotient AS FLOAT;
                      quotient = a / b;
                      SPEAK(""Sum: "" + sum);
                      SPEAK(""Product: "" + product);
                      SPEAK(""Quotient: "" + quotient);
                      SHARE 0;
                      DONE
                      DONE";
         */
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string dataFolderPath = Path.Combine(projectDirectory, "sample programs");
        string filename = "sample8.txt"; 

        string filePath = Path.Combine(dataFolderPath, filename);
        Console.WriteLine(filePath);
      //  Console.ReadLine();
        string input = ReadTextFromFile(filePath);
         
        input = ReadTextFromFile(filePath);

        Scanner scanner = new Scanner();
        List<Token> tokens = scanner.Tokenize(input);

        foreach (Token token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Lexeme: {token.Lexeme}");
        }
    }
}
