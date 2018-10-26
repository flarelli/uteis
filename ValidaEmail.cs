using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomacaoBDD.Functions
{
    public class ValidaEmail
    {
        
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }

    // The example displays the following output:
    //       Valid: david.jones@proseware.com
    //       Valid: d.j@server1.proseware.com
    //       Valid: jones@ms1.proseware.com
    //       Invalid: j.@server1.proseware.com
    //       Invalid: j@proseware.com9
    //       Valid: js#internal@proseware.com
    //       Valid: j_9@[129.126.118.1]
    //       Invalid: j..s@proseware.com
    //       Invalid: js*@proseware.com
    //       Invalid: js@proseware..com
    //       Invalid: js@proseware.com9
    //       Valid: j.s@server1.proseware.com

}
    
