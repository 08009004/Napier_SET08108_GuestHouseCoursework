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
        public static Dictionary<String, String> ReadData<T>(String filename)
        {
            List<String> csvInstances = read(filename);
            List<String> instance;
            int keyIndex = 0;
            int valueIndex = 0;
            Dictionary<String, String> instances = new Dictionary<string, string>();

            foreach (String csvInstance in csvInstances)
            {
                instance = separate(csvInstance);
                foreach (String s in instance)
                instances.Add(instance.ElementAt(keyIndex), instance.ElementAt(valueIndex));

            }

            return instances;
        }

        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         */
        // resource: https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
        private static List<String> read(String filename)
        {
            List<String> csvInstances = new List<String>();
            String line;

            try
            {   // Open the text file using a stream reader.
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();
                while (line != null)
                {
                    csvInstances.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
            }

            return csvInstances;
        }

        /*
         * Indexes a csv instance (string) into a dictionary<attribute, value>
         */
        public static Dictionary<String, String> index(String csvInstance)
        {
            Dictionary<String, String> indexedInstance = new Dictionary<string, string>();
            List<String> seperatedInstance = separate(csvInstance);
            if (seperatedInstance.Count % 2 != 0) ;// trow exception

            /* List<T>.GetRange(Int32, Int32) returns a shallow copy of a range 
             * of elements in the source List<T>.
             * https://msdn.microsoft.com/en-us/library/21k0e39c(v=vs.110).aspx
             */
            int size = seperatedInstance.Count;
            List<String> keys = seperatedInstance.GetRange(0, size/2);
            List<String> values = seperatedInstance.GetRange(size/2, size/2);

            for (int i = 0; i < size / 2; i++)
            {
                indexedInstance.Add(keys.ElementAt(i), values.ElementAt(i));
            }

            return indexedInstance;
        }

        /*
         * Separates each 'coma separated' field from the string passed
         * as a parameter.
         */
        private static List<String> separate(String csv)
        {
            List<String> separated = new List<String>(); 
            int nxtComaIndex = csv.IndexOf(",");

            /* String.IndexOf(Char) returns the zero-based index position of
             * value if that character is found, or -1 if it is not.
             * https://msdn.microsoft.com/en-us/library/kwb0bwyd(v=vs.110).aspx
             */
            while (nxtComaIndex > 0)
            {
                separated.Add(csv.Substring(0, nxtComaIndex));
                csv = csv.Substring(nxtComaIndex + 1, (csv.Length - (nxtComaIndex + 1)));
                nxtComaIndex = csv.IndexOf(",");
            }

            separated.Add(csv);
            return separated;
        }
    }
}
