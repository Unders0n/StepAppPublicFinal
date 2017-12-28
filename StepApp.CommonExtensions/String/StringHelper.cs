using System.Text.RegularExpressions;

namespace StepApp.CommonExtensions.String
{
   public static class StringHelper
    {
        public static string CamelCaseToSpaces(this string str)
        {
           return Regex.Replace(str, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
        }
    }
}
