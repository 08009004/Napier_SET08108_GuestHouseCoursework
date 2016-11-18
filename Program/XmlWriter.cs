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
    class XmlWriter
    {

        public static void Persist<T>(T obj)
        {
   //         try
   //         {
                writeData(@"holiday_reservation_system.xml", obj);
   //         }
   //         catch (Exception e)
   //         {
   //             Console.WriteLine("An error occurred:" + e.Message);
   //         }
        }


        private static void writeData<T>(String filepath, T obj)
        {
            StringBuilder xmlData = new StringBuilder();
            XmlSerializer s = new XmlSerializer(typeof(T));

            Console.WriteLine(filepath);
            Console.Read();
            TextWriter tw = new StreamWriter(filepath);
            s.Serialize(tw, obj);
            tw.Close(); // Cleanup
        }

    }
}
