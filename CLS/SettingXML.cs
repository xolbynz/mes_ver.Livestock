using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace 스마트팩토리.CLS
{
    public class SettingXML
    {
        private string setting_File;

        public SettingXML(string p_File)
        {
            setting_File = p_File;
        }

        // HashTable --> XML 파일로 저장
        public void WriteXML(Hashtable hTable)
        {
            IDictionaryEnumerator dicEnum = hTable.GetEnumerator();

            XmlTextWriter xmlWr = new XmlTextWriter(setting_File, Encoding.UTF8);
            xmlWr.Formatting = Formatting.Indented;
            xmlWr.WriteStartDocument();
            xmlWr.WriteStartElement("Configuration");

            while (dicEnum.MoveNext())
            {
                xmlWr.WriteElementString(dicEnum.Key.ToString(), dicEnum.Value.ToString());
            }

            xmlWr.WriteEndElement();
            xmlWr.WriteEndDocument();

            xmlWr.Flush();
            xmlWr.Close();
        }

        // XML 파일 --> HashTable 읽기
        public Hashtable ReadXml()
        {
            Hashtable ht = new Hashtable();
            string sKey;
            string sValue;

            try
            {
                XmlTextReader xmlRd = new XmlTextReader(setting_File);

                while (xmlRd.Read())
                {
                    if (xmlRd.NodeType == XmlNodeType.Element)
                    {
                        sKey = xmlRd.LocalName;

                        xmlRd.Read();

                        if (xmlRd.NodeType == XmlNodeType.Text)
                        {
                            sValue = xmlRd.Value;

                            ht.Add(sKey, sValue);
                        }
                    }
                }

                xmlRd.Close();
            }
            catch
            {
            }
            return ht;
        }

    }
}
