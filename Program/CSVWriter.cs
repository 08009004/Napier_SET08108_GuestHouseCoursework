using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Program
{
    /*
     * Static utility class to persist process objects to CSV files.
     */
    static class CSVWriter
    {
        /*
         * Selects the csv file where the data is persisted according on the
         * type of the object passed as a parameter.
         */
        public static void Persist<T>(T obj)
        {
            switch (typeof(T).Name)
            {
                case "Person": 
                    writeCSVData<T>(@"person.csv", obj);
                    break;
            }
        }

        /*
         * Persists obj.ToString() to the CSV file filename.
         */
        private static void writeCSVData<T>(String filename, T obj)
        {
            System.IO.File.AppendAllText(filename, obj.ToString());
        }
    }
}
