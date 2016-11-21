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
    enum PersonField {  FIRST_NAME, SECOND_NAME }
    enum CustomerField { CUSTOMER_NUMBER, ADDRESS }
    enum GuestField { PASSPORT_NUMBER, AGE }

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
        public static List<Dictionary<string, string>> ReadData(String filename)
        {
            List<String> csvInstances = aggregateInstances(readLines(filename));
            return indexInstances(csvInstances);
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
        public static List<String> aggregateInstances(List<String> fileLines)
        {
            List<String> csvInstances = new List<String>();
            String s;
            int i = 0;

            while (i < fileLines.Count)
            {
                s = String.Empty;

                do
                {
                    if (!fileLines.ElementAt(i).StartsWith("#")) throw new ArgumentException("write message");
                    
                    s += fileLines.ElementAt(i) + ",";
                    s += fileLines.ElementAt(i + 1) + ",";
                    
                    i = i + 2;
                } while (i < fileLines.Count
                         && !fileLines.ElementAt(i).StartsWith("#PERSON"));

                csvInstances.Add(s.Substring(0, s.Count() - 1));
            }

            return csvInstances;
        }

        /*
         * Indexes a list of csv instances (strings) into a list of 
         * dictionary<attribute, value>
         */
        private static List<Dictionary<string, string>> indexInstances(List<String> csvInstances)
        {
            List<Dictionary<string, string>> indexedInstances = new List<Dictionary<string, string>>();

            foreach (String csvInstance in csvInstances)
            {
                indexedInstances.Add(index(csvInstance));
            }
            return indexedInstances;
        }

        /*
         * Indexes a csv instance (string) into a dictionary<attribute, value>
         */
        public static Dictionary<String, String> index(String csvInstance)
        {
            Dictionary<String, String> indexedInstance = new Dictionary<string, string>();
            String[] seperatedInstance = separate(csvInstance);
            if (seperatedInstance.Length % 2 != 0) ;// trow exception

            for (int i = 0; i < seperatedInstance.Length; i++)
            {
                switch (seperatedInstance[i])
                {
                    case "#PERSON":
                        PersonField[] keysArr0 = (PersonField[]) Enum.GetValues(typeof(PersonField));
                        foreach (PersonField k in keysArr0) 
                        {
                            indexedInstance.Add(k.ToString(), seperatedInstance[i + Array.IndexOf(keysArr0, k) + 1]);
                        }
                        break;
                    case "#CUSTOMER":
                        CustomerField[] keysArr1 = (CustomerField[])Enum.GetValues(typeof(CustomerField));
                        foreach (CustomerField k in Enum.GetValues(typeof(CustomerField)))
                        {
                            indexedInstance.Add(k.ToString(), seperatedInstance[i + Array.IndexOf(keysArr1, k) + 1]);
                        }
                        break;
                    case "#GUEST":
                        GuestField[] keysArr2 = (GuestField[])Enum.GetValues(typeof(GuestField));
                        foreach (GuestField k in Enum.GetValues(typeof(GuestField)))
                        {
                            indexedInstance.Add(k.ToString(), seperatedInstance[i + Array.IndexOf(keysArr2, k) + 1]);
                        }
                        break;
                    default : break;
                }
            }

            return indexedInstance;
        }

        /*
         * Separates each 'coma separated' field from the string passed
         * as a parameter.
         */
        public static String[] separate(String csv)
        {
            return csv.Replace("\r\n", ",").Split(',');
        }
    }
}
