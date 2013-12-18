using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UException
    {
        public static string getExceptionMessage(string className, string methodName, string text)
        {
            return className + "." + methodName + " - " + text;
        }
    }
}
