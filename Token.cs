using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _219003234_Naidoo_KN_CC
{
    /// <summary>
    /// This class holds the definition of what a token is
    /// </summary>
   public class Token
    {
        public string Type { get; }
        public string Lexeme { get; }

        public Token(string type, string lexeme)
        {
            Type = type;
            Lexeme = lexeme;
        }
    }
}
