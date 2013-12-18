using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Lexnet.Security;

namespace com.lexnetcrm.common
{
    public class UEncryptDecrypt
    {
        public static string getEncryptedAppSettingValue1(string appSettingName)
        {
            if (String.IsNullOrEmpty(appSettingName) == true)
            {
                throw new ArgumentNullException("You must specify the name of the app setting you are trying to get");
            }

            string returnValue;

            if (Boolean.Parse(ConfigurationManager.AppSettings.Get("EncryptedCredentials")) == true)
            {
                returnValue = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings.Get(appSettingName));
            }
            else
            {
                returnValue = ConfigurationManager.AppSettings.Get(appSettingName);
            }

            return returnValue;
        }

        public static void setEncryptedAppSettingValue1(string appSettingName, string appSettingValue)
        {
            if (String.IsNullOrEmpty(appSettingName) == true)
            {
                throw new ArgumentNullException(UException.getExceptionMessage("UEncryptDecrypt", "setEncryptedAppSettingValue", "You must specify the name of the app setting you are trying to set"));
            }

            if (Boolean.Parse(ConfigurationManager.AppSettings.Get("EncryptedCredentials")) == true)
            {
                //UConfig.setAppConfigValue(appSettingName, EncryptDecrypt.Encrypt(ConfigurationManager.AppSettings.Get(appSettingValue)));
                //UConfig.setAppConfigValue(appSettingName, EncryptDecrypt.Encrypt(appSettingValue));
                UConfig.setAppConfigValue(appSettingName, appSettingValue, true);
            }
            else
            {
                //UConfig.setAppConfigValue(appSettingName, appSettingValue);
                UConfig.setAppConfigValue(appSettingName, appSettingValue, false);
            }
        }
    }
}
