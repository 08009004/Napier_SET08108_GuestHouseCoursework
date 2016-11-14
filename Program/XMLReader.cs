using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Program
{
    class XMLReader
    {
        private static Object readXML()
        {
            XmlSerializer s = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(@"holiday_camp.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
