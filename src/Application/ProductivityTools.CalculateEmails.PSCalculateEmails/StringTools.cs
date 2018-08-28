using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails
{
    public static class StringTools
    {
        public static string FillWithZeros(this string s, int amount)
        {
            return s.PadLeft(amount, '0');
        }
    }
}
