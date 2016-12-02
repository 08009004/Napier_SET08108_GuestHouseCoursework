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
         * Persists a BookingComponent to {dataDirectory}/{BOOKING_NUMBER}.csv 
         * Returns true if data was persisted successfuly or false if not.
         */
        public static bool Persist(BookingComponent booking)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory, 
                                             booking.GetBookingNb()));

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                System.IO.File.AppendAllText(filePath, booking.ToCSV());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
