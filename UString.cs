using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UString
    {
        public static string addQuotes(string value)
        {
            return "'" + value + "'";
        }

        public static string blankStringToNull(string value)
        {
            string returnValue;

            if (String.IsNullOrEmpty(value.Trim()) == true)
            {
                returnValue = "null";
            }
            else
            {
                returnValue = value;
            }

            return returnValue;
        }


        //Need to finish this 
        public static string trimCharacters(string value, string pattern)
        {
            if (String.IsNullOrEmpty(value) == true)
            {
                return value;                                   
            }

            if (String.IsNullOrEmpty(pattern) == true)
            {
                return value;
            }

            value = value.Trim();

            return value.Replace("\"", "");
        }

        public static string isNullOrEmptyToValue(string value, string valueIfNull)
        {
            if (String.IsNullOrEmpty(value) == true)
            {
                return valueIfNull;
            }
            else
            {
                return value;
            }
        }
    }
}
