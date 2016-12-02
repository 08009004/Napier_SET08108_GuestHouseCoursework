using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Utility class, facade for the Data Persistence implementation
     * details.
     */
    class DataPersistenceFacade
    {
        // Properties: 
        // the data persistence directory.
        private String dataDirectory = @"data";
        // the data writer instance:
        private CSVWriter dataWriter = CSVWriter.Instance;
        // the data reader instance:
        private CSVReader dataReader = CSVReader.Instance;

        /*
         * Persists the BookingComponent to {dataDirectory}/{bookingNb}.csv; 
         * returns true if data was persisted successfuly or false if not.
         */
        public bool Persist(BookingComponent booking)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory, 
                                             booking.GetBookingNb()));

            return dataWriter.Persist(booking, filePath);
        }

        /*
         * Returns a List<Dictionary<attribute, value>>, each representing
         * an entity of a given BookingComponent (the dictonary are named as 
         * defined in the *Field.cs enumerations).
         */
        public List<Dictionary<String, String>> Read(int bookingNb)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory,
                                             bookingNb));

            return dataReader.ReadBooking(filePath);
        }

    }
}
