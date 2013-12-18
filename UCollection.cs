using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UCollection
    {
        public static string getCollectionKey(string key)
        {
            if (String.IsNullOrEmpty(key) == true)
            {
                throw new ArgumentNullException(UException.getExceptionMessage("UCollection", "getCollectionKey", "A key is required"));
            }

            return key.Trim().ToUpper();
        }
    }
}
