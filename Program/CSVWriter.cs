using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.IO;

namespace Program
{
    /*
     * Static utility class, persists process objects data to CSV files.
     */
    static class CSVWriter
    {
        /* DEPRECATED
         * 
         * Selects and writes to the CSV file related to the type
         * of the object passed as a parameter.
         */
        /*
        public static void Persist<T>(T obj)
        {
            switch (typeof(T).Name)
            {
                case "Person": 
                    
                    break;
            }
        }
         */

        /*
         * Persists obj.ToString() to the CSV file filename.
         */
        public static void WriteData<T>(String filename, T obj)
        {
            System.IO.File.AppendAllText(filename, obj.ToString());
        }
    }
}
