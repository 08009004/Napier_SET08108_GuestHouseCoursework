using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.Xml.Serialization;
using System.IO;

namespace Program
{
    /*
     * 
     * 
     */
    // resource: https://msdn.microsoft.com/en-us/library/90c86ass(v=vs.110).aspx
    class XmlReader
    {
        public static List<T> RecoverAll<T>()
        {
            List<T> l = new List<T>();

            // Create a new XmlSerializer instance with the type of the test class
            XmlSerializer s = new XmlSerializer(typeof(T));

            // Create a new file stream for reading the XML file
            FileStream fs = new FileStream(@"holiday_reservation_system.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load the object saved above by using the Deserialize function
            T obj = (T) s.Deserialize(fs);
            
            // Cleanup
            fs.Close();

            l.Add(obj);

            return l;
        }
/*        private static Object readXML()
        {
            XmlSerializer s = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(@"holiday_camp.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        }
*/    }

}

