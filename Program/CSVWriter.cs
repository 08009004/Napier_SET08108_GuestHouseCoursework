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
        /*
        private static string personFile = @"person.csv";
        private static string bookingFile = @"booking.csv";

        public static void Persist(PersonComponent obj)
        {
            System.IO.File.AppendAllText(personFile, obj.ToCSV());
        }

        public static void Persist(BookingComponent obj)
        {
            System.IO.File.AppendAllText(bookingFile, obj.ToCSV());
        }

        /*
         * Persists obj.ToCSV() to the CSV file filename.
         */
        /*
        public static void WriteData(String filename, PersonComponent obj)
        {
            System.IO.File.AppendAllText(filename, obj.ToCSV());
        }
         */

        /*
         * Persists a BookingComponent to data/{BOOKING_NUMBER}.csv
         */
        public static bool Persist(BookingComponent booking)
        {
            System.IO.File.AppendAllText(String.Format(@"data/{0}.csv",booking.GetBookingNb()), booking.ToCSV());
            return false;
        }
    }
}
