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
         * Persists the BookingComponent to given filePath, returns true if 
         * data was persisted successfuly or false if not.
         */
        public static bool Persist(BookingComponent booking, String filePath)
        {
            String dataDirName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dataDirName))
            {
                Directory.CreateDirectory(dataDirName);
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
