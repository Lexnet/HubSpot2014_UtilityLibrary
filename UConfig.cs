using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using com.lexnetcrm.common;

using Lexnet.Security;

namespace com.lexnetcrm.common
{
    public class UConfig
    {
        /*
        public static void setAppConfigValue(string key, string value)
        {
            setAppConfigValue(key, value, false);
        }
        */

        public static void setAppConfigValue(string key, string value, bool encrypted)
        {            
            if (encrypted == true)
            {
                writeConfigValue(key, EncryptDecrypt.Encrypt(value));
            }
            else
            {
                writeConfigValue(key, value);
            }
        }        

        /*
        public static string getAppConfigValue(string key)
        {
            return getAppConfigValue(key, false);
        }
        */

        //May need to adjust this to return empty string for situations where the password is blank for example
        public static string getAppConfigValue(string key, bool encrypted)
        {
            if (String.IsNullOrEmpty(key) == true)
            {
                throw new ArgumentNullException("You must specify the name of the app setting you are trying to get");
            }

            //Console.WriteLine("KEY = " + key);
            
            if (encrypted == true)
            {
                return EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings.Get(key));
            }
            else
            {
                return ConfigurationManager.AppSettings.Get(key);
            }
        }
        /*
        private static string getEncryptedAppConfigValue(string key)
        {
            return UEncryptDecrypt.getEncryptedAppSettingValue(key);
        }
        
        private static void setEncryptedAppConfigValue(string key, string value)
        {
            UEncryptDecrypt.setEncryptedAppSettingValue(key, value);
        }
        */
        private static void writeConfigValue(string key, string value)
        {
            if (String.IsNullOrEmpty(key) == true)
            {
                throw new ArgumentNullException("You must specify the name of the app setting you are trying to set");
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string validateConfig()
        {
            string configError = "";

            if (UConfig.getAppConfigValue("EncryptedCredentials", false) == null) UConfig.setAppConfigValue("EncryptedCredentials", "TRUE", false);
            if (UConfig.getAppConfigValue("HubSpotURLxxx", false) == null) UConfig.setAppConfigValue("HubSpotURL", "https://api.hubapi.com/leads/v1/list?hapikey=", false);
            if (UConfig.getAppConfigValue("HubSpotAPIVersion", false) == null) UConfig.setAppConfigValue("HubSpotAPIVersion", "v1", false);
            if (UConfig.getAppConfigValue("userid", false) == null) UConfig.setAppConfigValue("userid", "ADMIN", false);
            if (UConfig.getAppConfigValue("ownerid", false) == null) UConfig.setAppConfigValue("ownerid", "SYST00000001", false);
            if (UConfig.getAppConfigValue("opp_closed_field", false) == null) UConfig.setAppConfigValue("opp_closed_field", "oppo_status", false);
            if (UConfig.getAppConfigValue("opp_closed_values", false) == null) UConfig.setAppConfigValue("opp_closed_values", "won", false);
            if (UConfig.getAppConfigValue("LastHubSpotPushDateTime", false) == null) UConfig.setAppConfigValue("LastHubSpotPushDateTime", "", false);
            if (UConfig.getAppConfigValue("PullDateTimeVariance", false) == null) UConfig.setAppConfigValue("PullDateTimeVariance", "0", false);
            if (UConfig.getAppConfigValue("DatabaseVersion", false) == null) UConfig.setAppConfigValue("DatabaseVersion", "SQL2005", false);
            if (UConfig.getAppConfigValue("AlwaysCreateAsLead", false) == null) UConfig.setAppConfigValue("AlwaysCreateAsLead", "false", false);
            if (UConfig.getAppConfigValue("LeadRecordCheck", false) == null) UConfig.setAppConfigValue("LeadRecordCheck", "GUIDEmail", false);
            if (UConfig.getAppConfigValue("PushChangesToHubSpot", false) == null) UConfig.setAppConfigValue("PushChangesToHubSpot", "FALSE", false);
            if (UConfig.getAppConfigValue("CRMSoftware", false) == null) UConfig.setAppConfigValue("CRMSoftware", "", false);
            if (UConfig.getAppConfigValue("HubSpotTimePivot", false) == null) UConfig.setAppConfigValue("HubSpotTimePivot", "lastModifiedAt", false);
            if (UConfig.getAppConfigValue("SageCRMSoapUrl", false) == null) UConfig.setAppConfigValue("SageCRMSoapUrl", "", false);
            if (UConfig.getAppConfigValue("LogLevel", false) == null) UConfig.setAppConfigValue("LogLevel", "Debug", false);
            if (UConfig.getAppConfigValue("lead_stage", false) == null) UConfig.setAppConfigValue("lead_stage", "New Lead", false);
            if (UConfig.getAppConfigValue("lead_source", false) == null) UConfig.setAppConfigValue("lead_source", "HubSpot", false);
            if (UConfig.getAppConfigValue("lead_description", false) == null) UConfig.setAppConfigValue("lead_description", "HubSpot Lea", false);
            if (UConfig.getAppConfigValue("LastHubSpotPullDateTime", false) == null) UConfig.setAppConfigValue("LastHubSpotPullDateTime", "", false);
            if (UConfig.getAppConfigValue("DedupeOnImport", false) == null) UConfig.setAppConfigValue("DedupeOnImport", "FALSE", false);
            if (UConfig.getAppConfigValue("LogDaysToKeep", false) == null) UConfig.setAppConfigValue("LogDaysToKeep", "30", false);

            if (UConfig.getAppConfigValue("HubSpotAPIKey", false) == null) UConfig.setAppConfigValue("HubSpotAPIKey", "", false);
            if (UConfig.getAppConfigValue("Username", false) == null) UConfig.setAppConfigValue("Username", "", false);
            if (UConfig.getAppConfigValue("Password", false) == null) UConfig.setAppConfigValue("Password", "", false);
            if (UConfig.getAppConfigValue("SQLConnectionString", false) == null) UConfig.setAppConfigValue("SQLConnectionString", "", false);
            if (UConfig.getAppConfigValue("OleDbConnectionString", false) == null) UConfig.setAppConfigValue("OleDbConnectionString", "", false);
            if (UConfig.getAppConfigValue("HubSpotBaseURL", false) == null) UConfig.setAppConfigValue("HubSpotBaseURL", "https://api.hubapi.com/", false);
            
            return configError;

            /*                
             
                <add key="HubSpotAPIKey" value="Yv5f22SyB3HMgE7krFGGwihPQHky7PqeUFL4E4BdBCuRiWxwl9KK3A==" />
                <add key="Username" value="jRrRMJhrkxY=" />
                <add key="Password" value="jRrRMJhrkxY=" />
                <add key="SQLConnectionString" value="3O3HhjBIjpOJ27JF9CGLjw8R9iIVQO1OVzgwbiV+mJZM83hFQrWwfCp+zSiurlO9GXJh7Hd/X0xWoH8VVCxM8qfK5YZiv+pC0POI2eigRvYA/mVLy/y+8q9MAQVvgHl9o9Ltu+vUGfyMxThkC3wCxLARgKS0oB0FFEccUOtJ5/I=" />
                <add key="OleDbConnectionString" value="GyiJUZeQMi+siuPzJiT1sL/wxElqQVimQXrgwaJD47th40yuC2R+s7BdhRTWHco6dfxOeH2tIU5dcH2ZqhxY+wTBSVYxpw1FH23UAyDJUHDml8vupCMceGwim9QnrSg+3Ncn2X4T6DugpSUbT9awOB9FgqUVfjX8NUB7MocFGxAULtMz++HoojKBsnZnqnJ6y9uiy8D1JUtrbbuOuY3eVEOmsZW0Brg4eadP/wiTnsR3P+i0DERmj46AbbS373k9rH6olnSKBSOP+RVZPa77sk5EK+m9lparw4iC8cQltXM=" />
                
            */

        }


    }
}
