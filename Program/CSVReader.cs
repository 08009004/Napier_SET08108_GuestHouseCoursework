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
    /* 
     * Enumeration, for each CSVWritable class, of its fields in the order
     * they appear in the persisted file.
     */
    enum PersonFields {  NAME, AGE }
    enum CustomerFields { F1 }
    enum GuestFields { F0, F1 }

    /* 
     * Static utility class, reads system objects data from CSV files.
     */
    class CSVReader
    {
        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         */
        // resource: https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
        public static Dictionary<String, String> ReadData<T>(String filename)
        {
            List<String> csvLines = readLines(filename);
 //           List<String[]> csvInstances = aggregatePerson(csvLines);

            return new Dictionary<string, string>();
        }

        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         */
        // resource: https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
        private static List<String> readLines(String filename)
        {
            List<String> csvLines = new List<String>();
            String line;

            try
            {   // Open the text file using a stream reader.
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();
                while (line != null)
                {
                    csvLines.Add(line);
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
                // TODO: throw exception here
            }

            return csvLines;
        }

        /*
         * Agregates csv lines into a list of strings, each element
         * of which represents a CSVWritable instance.
         */
        public static List<String> aggregateInstance(List<String> fileLines)
        {
            List<String> csvInstances = new List<String>();
            String s;
            int i = 0;
            int j = 0;

            while (i < fileLines.Count)
            {
                s = String.Empty;

                do
                {
                    if (!fileLines.ElementAt(i).StartsWith("#")) throw new ArgumentException("write message");
                    
                    s += fileLines.ElementAt(i);
                    s += fileLines.ElementAt(i + 1);
                    
                    i = i + 2;
                } while (i < fileLines.Count
                         && !fileLines.ElementAt(i).StartsWith("#PERSON"));

                csvInstances.Add(s);
            }

            return csvInstances;
        }

        /*
         * Separates each 'coma separated' field from the string passed
         * as a parameter.
         */
        private static String[] separate(String csv)
        {
            return csv.Split(',');
        }

        /*
         * Indexes a csv instance (string) into a dictionary<attribute, value>
         */
        public static Dictionary<String, String> index(String csvInstance)
        {
            Dictionary<String, String> indexedInstance = new Dictionary<string, string>();
            List<String> seperatedInstance = separate(csvInstance).ToList();
            if (seperatedInstance.Count % 2 != 0) ;// trow exception

            /* List<T>.GetRange(Int32, Int32) returns a shallow copy of a range 
             * of elements in the source List<T>.
             * https://msdn.microsoft.com/en-us/library/21k0e39c(v=vs.110).aspx
             */
            int size = seperatedInstance.Count;
            List<String> keys = seperatedInstance.GetRange(0, size / 2);
            List<String> values = seperatedInstance.GetRange(size / 2, size / 2);

            for (int i = 0; i < size / 2; i++)
            {
                indexedInstance.Add(keys.ElementAt(i), values.ElementAt(i));
            }

            return indexedInstance;
        }
    }
}
