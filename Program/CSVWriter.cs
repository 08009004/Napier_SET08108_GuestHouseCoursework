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
        
        private static string personFile = @"person.csv";
        private static string bookingFile = @"person.csv";

        public static void Persist(CSVWritable obj)
        {
            switch (obj.GetType().Name)
            {
                case "Person": 
                    WriteData(personFile, obj);
                    break;
            }
        }

        /*
         * Persists obj.ToString() to the CSV file filename.
         */
        public static void WriteData(String filename, CSVWritable obj)
        {
            System.IO.File.AppendAllText(filename, obj.ToCSV());
        }
    }
}
