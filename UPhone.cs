using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.lexnetcrm.common
{
    public class UPhone
    {
        public static string formatPhone(string phoneNumber, string country, string extension)
        {
            //Not currently using country to determine format. Could easily be added though

            if (String.IsNullOrEmpty(phoneNumber) == true)
            {
                return phoneNumber;
            }

            if (String.IsNullOrEmpty(country) == true)
            {
                //Assume US formatting
                country = "US";
            }

            if (String.IsNullOrEmpty(extension) == true)
            {
                //Assume x for extension
                extension = "x";
            }

            string numericPhoneNumber = getPhoneAsInt(phoneNumber);

            string formattedPhone = "";

            if (numericPhoneNumber.Length == 7)
            {
                //Assume no area code                
                formattedPhone = formatPhoneNumberNoAreaCode(getPhoneNumberWithoutAreaCode(numericPhoneNumber), null);
            }
            else if (numericPhoneNumber.Length == 10)
            {
                //Just format                
                formattedPhone = formatAreaCode(getAreaCode(numericPhoneNumber), null) + " " + formatPhoneNumberNoAreaCode(getPhoneNumberWithoutAreaCode(numericPhoneNumber), null);
            }
            else if (numericPhoneNumber.Length > 10)
            {
                //Format with extension string
                formattedPhone = formatAreaCode(getAreaCode(numericPhoneNumber), null) + " " + formatPhoneNumberNoAreaCode(getPhoneNumberWithoutAreaCode(numericPhoneNumber), null);
            }
            else
            {
                //Return what was passed
                formattedPhone = phoneNumber;
            }

            return formattedPhone;
        }

        public static string getFormattedAreaCode(string phoneNumber, string format)
        {
            return formatAreaCode(getAreaCode(phoneNumber), format);
        }

        public static string getAreaCode(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber) == true)
            {
                return phoneNumber;
            }

            string areaCode = "";

            //***** MHM Contacts – Added *****//
            phoneNumber = Regex.Replace(phoneNumber, @"\D", string.Empty);
            if (phoneNumber.Length > 3) // KVW Added 05172013 to deal with bogus phone numbers.
            {
                areaCode = phoneNumber.Substring(0, 3);
            }
            //***** MHM Contacts *************//


            //***** MHM Contacts – Deleted *****//
            //if (phoneNumber.Trim().Length >= 10)
            //{
            //    areaCode = phoneNumber.Substring(0, 3);
            //}
            //***** MHM Contacts *************//

            return areaCode;
        }

        public static string getFormattedPhoneNumberWithoutAreaCode(string phoneNumber, string format)
        {
            return formatPhoneNumberNoAreaCode(getPhoneNumberWithoutAreaCode(phoneNumber), format);
        }

        public static string getPhoneNumberWithoutAreaCode(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber) == true)
            {
                return phoneNumber;
            }

            phoneNumber = getPhoneAsInt(phoneNumber);

            string phoneNumberWithoutAreaCode = "";

            if (phoneNumber.Trim().Length >= 10)
            {
                if (phoneNumber.Trim().Length > 10)
                {                 
                    phoneNumberWithoutAreaCode = phoneNumber.Substring(3, phoneNumber.Length - 3);
                }
                else
                {                    
                    phoneNumberWithoutAreaCode = phoneNumber.Substring(3, 7);
                }
            }
            else if (phoneNumber.Trim().Length == 7)
            {
                phoneNumberWithoutAreaCode = phoneNumber;
            }

            return phoneNumberWithoutAreaCode;
        }

        public static string formatAreaCode(string areaCode, string format)
        {
            if (String.IsNullOrEmpty(areaCode) == true)
            {
                return areaCode;
            }

            if (String.IsNullOrEmpty(format) == true)
            {
                format = "({0})";
            }

            areaCode = getPhoneAsInt(areaCode);

            return String.Format(format, new string[] { areaCode });
        }

        public static string formatPhoneNumberNoAreaCode(string phoneNumberNoAreaCode, string format)
        {
            if (String.IsNullOrEmpty(phoneNumberNoAreaCode) == true)
            {
                return phoneNumberNoAreaCode;
            }

            if (String.IsNullOrEmpty(format) == true)
            {
                if (phoneNumberNoAreaCode.Length > 7)
                {
                    format = "{0}-{1}{2}{3}";
                }
                else
                {
                    format = "{0}-{1}";
                }
            }

            phoneNumberNoAreaCode = getPhoneAsInt(phoneNumberNoAreaCode);

            string formattedPhoneNumberNoAreaCode = "";

            if (phoneNumberNoAreaCode.Length > 7)
            {
                formattedPhoneNumberNoAreaCode = String.Format(format, new string[] { phoneNumberNoAreaCode.Substring(0, 3), phoneNumberNoAreaCode.Substring(3, 4), "x", phoneNumberNoAreaCode.Substring(7, phoneNumberNoAreaCode.Length - 7)});
            }
            else
            {
                formattedPhoneNumberNoAreaCode = String.Format(format, new string[] { phoneNumberNoAreaCode.Substring(0, 3), phoneNumberNoAreaCode.Substring(3, 4) });
            }

            return formattedPhoneNumberNoAreaCode;
        }

        public static string getPhoneAsInt(string phoneNumber)
        {
            string pattern = @"[\D]";
            
            string value = Regex.Replace(phoneNumber, pattern, "");
                        
            return value;
        }

        public static bool phoneContainsOnlyNumbers(String phoneNumber)
        {
            try
            {
                int.Parse(phoneNumber);
            }
            catch (FormatException e)
            {
                return false;
            }

            return true;
        }
    }
}
