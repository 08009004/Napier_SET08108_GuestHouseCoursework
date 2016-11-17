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

/*
 * Static utility class, recovers objects data from CSV files.
 */
namespace Program
{
    class CSVReader
    {

        /* DEPRECATED
         * 
         * Selects and reads all persisted instances from the CSV file
         * related to the type of the object passed as a parameter, and
         * returns a list of CSV instances (as strings).
         * Returns null if the type passed as a parameter is not persisted.
         */
        /*
        public static List<String> RecoverInstances<T>()
        {
            switch (typeof(T).Name)
            {
                case "Person":
                    return read<Person>(@"person.csv");
                default:
                    return null;
            }
        }
         */

        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         */
        // resource: https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
        public static List<String> ReadData<T>(String filename)
        {
            List<String> instances = new List<String>();
            String line;

            try
            {   // Open the text file using a stream reader.
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();
                while (line != null)
                {
                    instances.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
            }

            return instances;
        }
    }
}
