using _219003234_Naidoo_KN_CC;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// This is the main program class that acts as a starting point for the program to run
/// </summary>
class Program
{
    /// <summary>
    /// This method is used to read from a file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
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

    /// <summary>
    /// The main method that is the starting point
    /// This method loads a sample file from 1 to 8 from the sample programs folder
    /// each file contains one of the example sample programs that is included in the documentation
    /// it then performs the scanner operations by trying to match input to tokens with there underlying type and lexeme
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
 
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string dataFolderPath = Path.Combine(projectDirectory, "sample programs");
        string filename = "sample8.txt";

        Console.WriteLine("Enter number to load file from 1 to 8 (anyother input will result in file 8 being loaded):");
        string fileNumber = Console.ReadLine();

        switch (fileNumber)
        {
            case "1":
                filename = "sample1.txt";
                break;
            case "2":
                filename = "sample2.txt";
                break;
            case "3":
                filename = "sample3.txt";
                break;
            case "4":
                filename = "sample4.txt";
                break;
            case "5":
                filename = "sample5.txt";
                break;
            case "6":
                filename = "sample6.txt";
                break;
            case "7":
                filename = "sample7.txt";
                break;
            case "8":
                filename = "sample8.txt";
                break;
            default:
                filename = "sample8.txt";
                break;
        }


        string filePath = Path.Combine(dataFolderPath, filename);
        Console.WriteLine(filePath);
     
        string input = ReadTextFromFile(filePath);
         
        input = ReadTextFromFile(filePath);


        //This prints the contents from the file to the screen
        Console.WriteLine(input);
        Console.WriteLine("\n");
     
        
        //This prints out the type and lexeme pairs from the input class
        Scanner scanner = new Scanner();
        List<Token> tokens = scanner.Tokenize(input);

        foreach (Token token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Lexeme: {token.Lexeme}");
        }
    }
}
