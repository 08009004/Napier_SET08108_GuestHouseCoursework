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
        // Property: the data persistence directory.
        private static String dataDirectory = @"data";

        /*
         * Persists a BookingComponent to 
         * {dataDirectory}/{BOOKING_NUMBER}.csv
         */
        public static bool Persist(BookingComponent booking)
        {
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            System.IO.File.AppendAllText(String.Format(@"{0}/{1}.csv", dataDirectory, booking.GetBookingNb()), booking.ToCSV());
            return false;
        }
    }
}
