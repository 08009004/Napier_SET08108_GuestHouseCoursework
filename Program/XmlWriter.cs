using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace Program
{
    class XmlWriter
    {
        private static string personFile = @"person.xml";

        public static void Persist<T>(T obj)
        {
   //         try
   //         {
                writeData(personFile, obj);
   //         }
   //         catch (Exception e)
   //         {
   //             Console.WriteLine("An error occurred:" + e.Message);
   //         }
        }


        private static void writeData<T>(String filepath, T obj)
        {
            StringBuilder xmlData = new StringBuilder();

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                Console.WriteLine(filepath);
                Console.Read();
                TextWriter tw = new StreamWriter(filepath);
                s.Serialize(tw, obj);
                tw.Close(); // Cleanup
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            

            
        }

    }
}
