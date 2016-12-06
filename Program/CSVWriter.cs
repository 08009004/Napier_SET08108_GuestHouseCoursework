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
     * Singleton utility class, persists booking data to CSV files.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class CSVWriter
    {
        // Properties: 
        private static CSVWriter instance;
        public static CSVWriter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CSVWriter();
                }
                return instance;
            }
        }

        /*
         * Private constructor, to prevent class instantiation from
         * external classes (singleton class).
         */
        private CSVWriter() { }

        /*
         * Persists the BookingComponent to given filePath, returns true if 
         * data was persisted successfuly or false if not.
         */
        public bool Persist(BookingComponent booking, String filePath)
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

        /*
         * Persists the PersonFactory to given filePath, returns true if 
         * data was persisted successfuly or false if not.
         */
        public bool Persist(PersonFactory pf, String filePath)
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
                System.IO.File.AppendAllText(filePath, pf.ToCSV());
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*
         * Persists the PersonFactory to given filePath, returns true if 
         * data was persisted successfuly or false if not.
         */
        public bool Persist(BookingFactory bf, String filePath)
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
                System.IO.File.AppendAllText(filePath, bf.ToCSV());
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
