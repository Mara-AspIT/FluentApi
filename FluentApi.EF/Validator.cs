using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi.EF
{
    public static class Validator
    {
        public static bool IsNameValid(string s)
        {
            if(String.IsNullOrWhiteSpace(s))
                return false;
            else if(s.Length > 50)
                return false;
            else if(!s.All(c => Char.IsLetter(c) && Char.IsSeparator(c)))
                return false;
            else
                return true;
        }
    }
}
