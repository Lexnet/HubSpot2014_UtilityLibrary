using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UBoolean
    {
        public enum BooleanFormat
        {
            SINGLECHAR,
            FULLVALUE
        }

        public static string formatBoolean(object value)
        {
            return formatBoolean(value, BooleanFormat.SINGLECHAR);
        }

        public static string formatBoolean(object value, BooleanFormat format)
        {
            string returnValue = null;
            string stringValue = value.ToString();

            if (value is string)
            {
                switch (stringValue.Trim().ToUpper())
                {
                    case "T":
                        returnValue = "TRUE";
                        break;
                    case "TRUE":
                        returnValue = "TRUE";
                        break;
                    case "Y":
                        returnValue = "TRUE";
                        break;
                    case "YES":
                        returnValue = "TRUE";
                        break;
                    case "1":
                        returnValue = "TRUE";
                        break;
                    default:
                        returnValue = "FALSE";
                        break;
                }
            }
            else if (value is int)
            {
                int intValue = int.Parse(stringValue);

                switch (intValue)
                {
                    case 1:
                        returnValue = "TRUE";
                        break;                    
                    default:
                        returnValue = "FALSE";
                        break;
                }
            }
            else if (value is bool)
            {
                bool boolValue = bool.Parse(stringValue);

                switch (boolValue)
                {
                    case true:
                        returnValue = "TRUE";
                        break;
                    default:
                        returnValue = "FALSE";
                        break;
                }
            }

            return convertBooleanFormat(bool.Parse(returnValue), format);
        }        

        public static string convertBooleanFormat(bool value, BooleanFormat format)
        {
            string returnValue;
            
            switch (format)
            {
                case BooleanFormat.FULLVALUE:
                    if (value == true)
                    {
                        returnValue = "TRUE";
                    }
                    else
                    {
                        returnValue = "FALSE";
                    }

                    break;
                case BooleanFormat.SINGLECHAR:
                    if (value == true)
                    {
                        returnValue = "T";
                    }
                    else
                    {
                        returnValue = "F";
                    }

                    break;
                default:
                    if (value == true)
                    {
                        returnValue = "T";
                    }
                    else
                    {
                        returnValue = "F";
                    }

                    break;
            }

            return returnValue;
        }

        public static bool parse(string value)
        {
            if (isValidBoolean(value) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isValidBoolean(object value)
        {
            try
            {
                return bool.Parse(value.ToString());
            }
            catch
            {
                return false;
            }
        }
    }
}
