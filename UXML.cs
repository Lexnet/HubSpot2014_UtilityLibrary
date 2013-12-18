using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace com.lexnetcrm.common
{
    public class UXML
    {
        public static XmlNodeList getXmlNodeList(XmlDocument xmlDoc, string xmlNodePath)
        {
            if (xmlDoc == null)
            {
                //Throw an exception
            }

            XmlNodeList nodes = null;

            if (String.IsNullOrEmpty(xmlNodePath) == false)
            {
                nodes = xmlDoc.SelectNodes(xmlNodePath);
            }

            return nodes;
        }

        public static XmlNode getXmlNode(XmlDocument xmlDoc, string xmlNodePath)
        {
            if (xmlDoc == null)
            {
                //Throw an exception
            }

            XmlNode node = null;

            if (String.IsNullOrEmpty(xmlNodePath) == false)
            {
                node = xmlDoc.SelectSingleNode(xmlNodePath);
            }

            return node;
        }
    }
}
