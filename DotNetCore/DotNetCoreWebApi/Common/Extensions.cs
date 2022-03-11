using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Common
{
    public static class Extensions
    {
        public static bool IsSpecialCharacter(this Char character)
        {
            string specialCharacters = "!#$%^&*()_-+=.,;`~";
            return specialCharacters.Contains(character);
        }
    }
}
