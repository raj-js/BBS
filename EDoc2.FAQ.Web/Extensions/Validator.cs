using System.Text.RegularExpressions;

namespace EDoc2.FAQ.Web.Extensions
{
    public class Validator
    {
        public static bool CheckEmail(string input)
        {
            var reg = new Regex(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");
            return reg.IsMatch(input);
        }
    }
}
