using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UFileSystem
    {
        public static void CreateDirectory(string @path, string directory)
        {
            string directoryPath = path;

            if (IsValidPath(path) == false)
            {
                directoryPath += "\\";
            }

            directoryPath += directory;

            if (Directory.Exists(path))
            {
                if (Directory.Exists(path + "\\" + directory))
                {

                }
                else
                {
                    Directory.CreateDirectory(path + "\\" + directory);
                }
            }
            else
            {

            }
        }

        public static string ReplaceInvalidFolderCharacters(string folderName)
        {
            string returnValue;
            string[] invalidCharacters = new string[10];

            invalidCharacters[0] = "\\";
            invalidCharacters[1] = "/";
            invalidCharacters[2] = ":";
            invalidCharacters[3] = "*";
            invalidCharacters[4] = "?";
            invalidCharacters[5] = "<";
            invalidCharacters[6] = ">";
            invalidCharacters[7] = "|";
            invalidCharacters[8] = "\"";
            invalidCharacters[9] = ",";

            returnValue = folderName;

            for (int i = 0; i < invalidCharacters.Length; i++)
            {
                if (returnValue.Contains(invalidCharacters[i]))
                {
                    returnValue = returnValue.Replace(invalidCharacters[i], "");
                }
            }

            return returnValue;
        }

        public static bool IsValidPath(string path)
        {
            if (path.EndsWith("\\"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ValidateDirectoryPath(string path)
        {
            if (IsValidPath(path) == true)
            {
                return path;
            }
            else
            {
                return path + "\\";
            }
        }

        public static string CalculatefileSize(long sizeInBytes)
        {
            string returnValue;

            if (sizeInBytes < 1024568)
            {
                returnValue = Convert.ToString(sizeInBytes / 1024) + " Kb";
            }
            else
            {
                if (sizeInBytes < 1024568000)
                {
                    returnValue = Convert.ToString((sizeInBytes / 1024568) + "." + (sizeInBytes % 1024568)) + " Mb";
                }
                else
                {
                    returnValue = Convert.ToString(sizeInBytes / 1024568000) + " Gb";
                }
            }

            return returnValue;
        }
    }
}
