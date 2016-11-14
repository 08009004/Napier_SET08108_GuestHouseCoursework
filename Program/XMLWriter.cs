using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Program
{
    /*
     * 
     * http://stackoverflow.com/questions/4123590/serialize-an-object-to-xml
     */
    class XMLWriter
    {
        public void AlgorithmInterface<T>(T obj)
        {
            try
            {
      //          writeCVSFile(data.FlightCode, generateCSVData(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred:" + e.Message);
            }
        }
        
        private String generateXmlData<T>(T value)
        {
            if (value == null) return string.Empty;  // Short circuit method if null object was passed
            
            StringBuilder xmlData = new StringBuilder();
            XmlSerializer s = new XmlSerializer(typeof(T));

            TextWriter WriteFileStream = new StreamWriter(@"h:\test.xml");
            s.Serialize(WriteFileStream, value);

            WriteFileStream.Close(); // Cleanup
            
            return xmlData.ToString();
        }

        private void writeXmlFile(String code, String xml)
        {
            System.IO.File.AppendAllText(code + ".xml", xml);
        }
    }
}
