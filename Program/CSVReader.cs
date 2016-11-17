using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.IO;
using System.Windows;  // temp (dev)

// NOTE FOR GUI: open file dialogue
// http://www.wpf-tutorial.com/dialogs/the-openfiledialog/
// NOTE FOR STARTUP:
// http://stackoverflow.com/questions/6301529/open-a-text-file-with-wpf
//
namespace Program
{
    class CSVReader
    {
        /*
         * 
         * https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
         */
        public static void Read(String filename)
        {
            try
            {   // Open the text file using a stream reader.
                StreamReader sr = new StreamReader(filename);
                String line = sr.ReadToEnd();
                MessageBox.Show(line);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
