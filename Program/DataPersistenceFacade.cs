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

    }
}
