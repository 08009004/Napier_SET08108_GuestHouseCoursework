using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.IO;

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
     * Static utility class, reads system objects data from CSV files.
     */
    class CSVReader
    {
        /*
         * Returns a list of Dictionary<string, string>, each of which is
         * a representation of of an object as <attribute, value>.
         * The dictonary keys follow the naming implemented in the *Field.cs
         * enumerations.
         */
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

            StreamReader sr = new StreamReader(filename);
            line = sr.ReadLine();
            while (line != null)
            {
                csvLines.Add(line);
                line = sr.ReadLine();
            }

            sr.Close();
            return csvLines;
        }

        /*
         * Agregates csv lines into a list of strings, each element
         * of which represents a CSVWritable instance.
         */
        private static List<String> aggregateInstances(List<String> fileLines)
        {
            List<String> csvInstances = new List<String>();
            String s;
            int i = 0;

            while (i < fileLines.Count)
            {
                s = String.Empty;

                do
                {
                    if (!fileLines.ElementAt(i).StartsWith("#"))
                    {
                        throw new ArgumentException("Invalid CSV file content");
                    }
                    
                    s += fileLines.ElementAt(i) + ",";
                    s += fileLines.ElementAt(i + 1) + ",";
                    
                    i = i + 2;
                } while (i < fileLines.Count
                         && !fileLines.ElementAt(i).StartsWith("#BOOKING"));

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
            List<Dictionary<string, string>> indexedInstances 
                                        = new List<Dictionary<string, string>>();

            foreach (String csvInstance in csvInstances)
            {
                indexedInstances.Add(index(csvInstance));
            }
            return indexedInstances;
        }

        /*
         * Indexes a csv instance (string) into a dictionary<attribute, value> 
         * instance
         */
        private static Dictionary<String, String> index(String csvInstance)
        {
            Dictionary<String, String> indexedInstance 
                                        = new Dictionary<string, string>();
            String[] seperatedInstance = csvInstance.Split(',');
            if (seperatedInstance.Length % 2 != 0) ;// trow exception

            for (int i = 0; i < seperatedInstance.Length; i++)
            {
                switch (seperatedInstance[i])
                {
                    case "#BOOKING":
                        BookingField[] keysArr3 = (BookingField[])Enum.GetValues(typeof(BookingField));
                        foreach (BookingField k in Enum.GetValues(typeof(BookingField)))
                        {
                            indexedInstance.Add(
                                k.ToString(),
                                seperatedInstance[i + Array.IndexOf(keysArr3, k) + 1]);
                        }
                        break;
                    case "#BREAKFAST":
                        BreakfastField[] keysArr4 = (BreakfastField[])Enum.GetValues(typeof(BreakfastField));
                        foreach (BreakfastField k in Enum.GetValues(typeof(BreakfastField)))
                        {
                            indexedInstance.Add(
                                k.ToString(),
                                seperatedInstance[i + Array.IndexOf(keysArr4, k) + 1]);
                        }
                        break;
                    case "#EVENING_MEAL":
                        EveningMealField[] keysArr5 = (EveningMealField[])Enum.GetValues(typeof(EveningMealField));
                        foreach (EveningMealField k in Enum.GetValues(typeof(EveningMealField)))
                        {
                            indexedInstance.Add(
                                k.ToString(),
                                seperatedInstance[i + Array.IndexOf(keysArr5, k) + 1]);
                        }
                        break;
                    case "#CAR_HIRE":
                        CarHireField[] keysArr6 = (CarHireField[])Enum.GetValues(typeof(CarHireField));
                        foreach (CarHireField k in Enum.GetValues(typeof(CarHireField)))
                        {
                            indexedInstance.Add(
                                k.ToString(),
                                seperatedInstance[i + Array.IndexOf(keysArr6, k) + 1]);
                        }
                        break;
                    case "#PERSON":
                        PersonField[] keysArr0 = (PersonField[]) Enum.GetValues(typeof(PersonField));
                        foreach (PersonField k in keysArr0) 
                        {
                            indexedInstance.Add(
                                k.ToString(), 
                                seperatedInstance[i + Array.IndexOf(keysArr0, k) + 1]);
                        }
                        break;
                    case "#CUSTOMER":
                        CustomerField[] keysArr1 = (CustomerField[])Enum.GetValues(typeof(CustomerField));
                        foreach (CustomerField k in Enum.GetValues(typeof(CustomerField)))
                        {
                            indexedInstance.Add(
                                k.ToString(), 
                                seperatedInstance[i + Array.IndexOf(keysArr1, k) + 1]);
                        }
                        break;
                    case "#GUEST":
                        GuestField[] keysArr2 = (GuestField[])Enum.GetValues(typeof(GuestField));
                        foreach (GuestField k in Enum.GetValues(typeof(GuestField)))
                        {
                            indexedInstance.Add(
                                k.ToString(), 
                                seperatedInstance[i + Array.IndexOf(keysArr2, k) + 1]);
                        }
                        break;
                    default : 
                        break;
                }
            }

            return indexedInstance;
        }
    }
}
